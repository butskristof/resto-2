using Resto.Domain.Common;
using Resto.Domain.Entities.Products;

namespace Resto.Domain.Entities.Orders;

public sealed class OrderLineTopping : AuditableBaseEntity<Guid>
{
	public Guid OrderLineId { get; set; }
	
	public Guid ToppingId { get; set; }
	public Topping Topping { get; set; }
}