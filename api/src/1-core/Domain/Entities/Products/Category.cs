using Resto.Domain.Common;

namespace Resto.Domain.Entities.Products;

public class Category : AuditableBaseEntity<Guid>
{
	public string Name { get; set; }
	public string Color { get; set; }
}