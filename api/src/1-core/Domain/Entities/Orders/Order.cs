using Resto.Domain.Common;
using Resto.Domain.Enumerations;

namespace Resto.Domain.Entities.Orders;

public sealed class Order : AuditableBaseEntity<Guid>
{
	public ICollection<OrderLine> OrderLines { get; set; }
	public OrderDiscount Discount { get; set; }
	public DateTimeOffset Timestamp { get; set; }

	public decimal OrderTotal
		=> Discount is OrderDiscount.Member or OrderDiscount.Volunteer
			? 0
			: OrderLines.Sum(ol => ol.OrderLineTotal);
}