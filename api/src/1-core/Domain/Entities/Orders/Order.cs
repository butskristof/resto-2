using Resto.Domain.Common;
using Resto.Domain.Enumerations;

namespace Resto.Domain.Entities.Orders;

public class Order : AuditableBaseEntity<Guid>
{
	public ICollection<OrderLine> OrderLines { get; set; }
	public OrderDiscount Discount { get; set; }
	public DateTime Timestamp { get; set; }
}