using Resto.Domain.Common;

namespace Resto.Domain.Entities.Products;

public sealed class Topping : AuditableBaseEntity<Guid>
{
	public string Name { get; set; }
	public decimal Price { get; set; }
}