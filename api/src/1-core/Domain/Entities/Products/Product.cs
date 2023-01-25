using Resto.Domain.Common;

namespace Resto.Domain.Entities.Products;

public class Product : AuditableBaseEntity<Guid>
{
	public string Name { get; set; }
	public decimal Price { get; set; }
	public bool MultipleToppingsAllowed { get; set; }

	public Guid CategoryId { get; set; }
	public Category Category { get; set; }
	
	public ICollection<ProductTopping> Toppings { get; set; }
}