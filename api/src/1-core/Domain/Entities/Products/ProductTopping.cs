using Resto.Domain.Common;

namespace Resto.Domain.Entities.Products;

public sealed class ProductTopping : AuditableBaseEntity<Guid>
{
	public Guid ProductId { get; set; }
	public Guid ToppingId { get; set; }
	public Topping Topping { get; set; }
}