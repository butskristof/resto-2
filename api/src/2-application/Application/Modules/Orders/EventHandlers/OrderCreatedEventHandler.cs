using MediatR;
using Microsoft.Extensions.Logging;
using Resto.Common.Integrations.TicketPrinting;
using Resto.Common.Integrations.TicketPrinting.Models;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Events;

namespace Resto.Application.Modules.Orders.EventHandlers;

internal class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
	#region construction

	private readonly ILogger<OrderCreatedEventHandler> _logger;
	private readonly ITicketPrintingService _ticketPrintingService;

	public OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger, ITicketPrintingService ticketPrintingService)
	{
		_logger = logger;
		_ticketPrintingService = ticketPrintingService;
	}

	#endregion

	public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
	{
		_logger.LogDebug("Handling OrderCreatedEvent for order with id {OrderId}", notification.Order.Id);

		await PrintOrderTicketAsync(notification.Order, cancellationToken);
		_logger.LogDebug("Printed ticket");
	}

	private Task PrintOrderTicketAsync(Order order, CancellationToken cancellationToken)
	{
		_logger.LogDebug("Printing ticket for order with id {OrderId}", order.Id);
		OrderTicketData data = null;
		return _ticketPrintingService.PrintOrderTicketAsync(data, cancellationToken);
	}
}