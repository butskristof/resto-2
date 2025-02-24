using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Mapping;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;
using Resto.Common.Integrations.TicketPrinting;
using Resto.Domain.Enumerations;

namespace Resto.Application.Modules.Orders;

public static class CreateOrder
{
    public sealed record Request(
        OrderDiscount Discount
    ) : IRequest<Response>
    {
        public IEnumerable<OrderLineRequest> OrderLines { get; init; } = [];

        public sealed record OrderLineRequest(
            Guid ProductId,
            int Quantity)
        {
            public IEnumerable<Guid> ToppingIds { get; init; } = [];
        }
    }

    public sealed record Response(Guid Id);

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator(IAppDbContext dbContext)
        {
            RuleFor(r => r.Discount)
                .IsInEnum()
                .WithErrorCode(ErrorCode.Invalid);

            RuleFor(r => r.OrderLines)
                .NotEmpty()
                .WithErrorCode(ErrorCode.Required);

            RuleForEach(r => r.OrderLines)
                .SetValidator(new OrderLineValidator(dbContext));
        }

        internal class OrderLineValidator : AbstractValidator<Request.OrderLineRequest>
        {
            public OrderLineValidator(IAppDbContext dbContext)
            {
                RuleFor(ol => ol.ProductId)
                    .NotEmpty().WithErrorCode(ErrorCode.Invalid)
                    .MustAsync(dbContext.ProductExistsByIdAsync).WithErrorCode(ErrorCode.NotFound);

                RuleFor(ol => ol.ToppingIds)
                    .Must(c => c.Count() <= 1)
                    .WhenAsync((ol, ct) => dbContext.ProductDoesNotAllowMultipleToppingsAsync(ol.ProductId, ct));

                RuleForEach(ol => ol.ToppingIds)
                    .NotEmpty().WithErrorCode(ErrorCode.Invalid) // guid itself, e.g. 0000-00-00-0000
                    .MustAsync(dbContext.ToppingExistsByIdAsync).WithErrorCode(ErrorCode.NotFound);

                RuleFor(ol => ol.Quantity)
                    .GreaterThan(0)
                    .WithErrorCode(ErrorCode.Invalid);
            }
        }
    }

    internal sealed class Handler : IRequestHandler<Request, Response>
    {
        #region construction

        private readonly ILogger<Handler> _logger;
        private readonly IAppDbContext _dbContext;
        private readonly TimeProvider _timeProvider;
        private readonly ITicketPrintingService _ticketPrintingService;

        public Handler(ILogger<Handler> logger, IAppDbContext dbContext, TimeProvider timeProvider,
            ITicketPrintingService ticketPrintingService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _timeProvider = timeProvider;
            _ticketPrintingService = ticketPrintingService;
        }

        #endregion

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Adding a new order");

            var order = request.MapToOrder();
            order.Timestamp = _timeProvider.GetLocalNow();
            _logger.LogDebug("Mapped request to entity type");

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            _logger.LogDebug("Persisted new order to database");

            try
            {
                _logger.LogDebug("Trying to print ticket for newly created order");

                // TODO refactor as projection?
                await _dbContext
                    .Entry(order)
                    .Collection(o => o.OrderLines)
                    .Query()
                    .Include(ol => ol.Product)
                    .Include(ol => ol.Toppings)
                    .ThenInclude(olt => olt.Topping)
                    .LoadAsync(cancellationToken);
                _logger.LogDebug("Fetched related information for new order from database");

                var ticketData = order.MapToOrderTicketData();
                _logger.LogDebug("Mapped order to ticket data");

                await _ticketPrintingService.PrintOrderTicketAsync(ticketData, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to print ticket for Order with ID {OrderId} after creation",
                    order.Id);
            }

            return new Response(order.Id);
        }
    }
}