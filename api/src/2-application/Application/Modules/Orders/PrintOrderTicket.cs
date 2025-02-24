using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Mapping;
using Resto.Application.Common.Persistence;
using Resto.Common.Enumerations;
using Resto.Common.Exceptions;
using Resto.Common.Integrations.TicketPrinting;

namespace Resto.Application.Modules.Orders;

public static class PrintOrderTicket
{
	public sealed record Request(Guid OrderId) : IRequest;

	internal sealed class Validator : AbstractValidator<Request>
	{
		public Validator()
		{
			RuleFor(r => r.OrderId)
				.NotEmpty().WithErrorCode(ErrorCode.Required);
		}
	}

	internal sealed class Handler : IRequestHandler<Request>
	{
		#region construction

		private readonly ILogger<Handler> _logger;
		private readonly IAppDbContext _dbContext;
		private readonly ITicketPrintingService _ticketPrintingService;

		public Handler(ILogger<Handler> logger, IAppDbContext dbContext, ITicketPrintingService ticketPrintingService)
		{
			_logger = logger;
			_dbContext = dbContext;
			_ticketPrintingService = ticketPrintingService;
		}

		#endregion

		public async Task Handle(Request request, CancellationToken cancellationToken)
		{
			_logger.LogDebug("Printing ticket for Order with ID {OrderId}", request.OrderId);

			var orderTicketData = await _dbContext
				.OrdersBaseQuery(false)
				.MapToOrderTicketData()
				.SingleOrDefaultAsync(o => o.Id == request.OrderId,
					cancellationToken: cancellationToken)
				?? throw new NotFoundException($"Could not find order with id {request.OrderId}");
			
			await _ticketPrintingService.PrintOrderTicketAsync(orderTicketData, cancellationToken);
		}
	}
}