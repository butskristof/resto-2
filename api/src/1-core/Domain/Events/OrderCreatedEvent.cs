using Resto.Domain.Common;
using Resto.Domain.Entities.Orders;

namespace Resto.Domain.Events;

public class OrderCreatedEvent : BaseEvent
{
	public OrderCreatedEvent(Order order)
	{
		Order = order;
	}

	public Order Order { get; }
}