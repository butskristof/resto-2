using Resto.Domain.Common;

namespace Resto.Domain.Entities.Orders;

public class OrderLine : AuditableBaseEntity<Guid>
{
	public Guid OrderId { get; set; }
	public Guid ProductId { get; set; }
	public ICollection<OrderLineTopping> Toppings { get; set; }
	public int Quantity { get; set; }
}