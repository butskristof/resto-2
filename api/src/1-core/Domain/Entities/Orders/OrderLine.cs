using Resto.Domain.Common;
using Resto.Domain.Entities.Products;

namespace Resto.Domain.Entities.Orders;

public class OrderLine : AuditableBaseEntity<Guid>
{
	public Guid OrderId { get; set; }
	
	public Guid ProductId { get; set; }
	public Product Product { get; set; }
	
	public ICollection<OrderLineTopping> Toppings { get; set; }
	public int Quantity { get; set; }
	
	public decimal Price => Product.Price + Toppings.Sum(t => t.Topping.Price);

	public decimal OrderLineTotal => Price * Quantity;
}