using Resto.Domain.Common;

namespace Resto.Domain.Entities.Products;

public class Product : BaseEntity
{
	public string Name { get; set; }
	public decimal Price { get; set; }
	public bool MultipleToppings { get; set; }

	public Guid CategoryId { get; set; }
	public Category Category { get; set; }
	
	public ICollection<ProductTopping> Toppings { get; set; }
}