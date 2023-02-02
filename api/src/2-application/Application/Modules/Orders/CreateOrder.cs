using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;
using Resto.Common.Services;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Events;

namespace Resto.Application.Modules.Orders;

public static class CreateOrder
{
	public class Request : IRequest<Response>
	{
		public IEnumerable<OrderLineRequest> OrderLines { get; set; }

		public class OrderLineRequest
		{
			public Guid ProductId { get; set; }
			public IEnumerable<Guid> ToppingIds { get; set; } = new List<Guid>();
			public int Quantity { get; set; }
		}
	}

	public record Response(Guid Id);

	internal class Validator : AbstractValidator<Request>
	{
		public Validator(IAppDbContext dbContext)
		{
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

	internal class Handler : IRequestHandler<Request, Response>
	{
		#region construction

		private readonly ILogger<Handler> _logger;
		private readonly IAppDbContext _dbContext;
		private readonly IMapper _mapper;
		private readonly IDateTime _dateTime;

		public Handler(ILogger<Handler> logger, IAppDbContext dbContext, IMapper mapper, IDateTime dateTime)
		{
			_logger = logger;
			_dbContext = dbContext;
			_mapper = mapper;
			_dateTime = dateTime;
		}

		#endregion

		public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
		{
			_logger.LogDebug("Adding a new order");

			var order = _mapper.Map<Order>(request);
			order.Timestamp = _dateTime.Now;
			order.AddDomainEvent(new OrderCreatedEvent(order));
			_logger.LogDebug("Mapped request to entity type");

			_dbContext.Orders.Add(order);
			await _dbContext.SaveChangesAsync();
			_logger.LogDebug("Persisted new order to database");
			
			// TODO print ticket

			return new Response(order.Id);
		}
	}
}