using Resto.Domain.Common;

namespace Resto.Domain.Entities.Orders;

public class OrderLineTopping : AuditableBaseEntity<Guid>
{
	public Guid OrderLineId { get; set; }
	public Guid ToppingId { get; set; }
}