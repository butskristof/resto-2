namespace Resto.Common.Integrations.TicketPrinting.Models;

public sealed class OrderTicketData
{
	public Guid Id { get; set; }
	public DateTimeOffset Timestamp { get; set; }
	public OrderTicketDiscount Discount { get; set; }

	public IEnumerable<OrderTicketOrderLine> OrderLines { get; set; }

	public decimal OrderTotal { get; set; }

	public enum OrderTicketDiscount
	{
		None = 0,
		Member = 1,
		Volunteer = 2,
	}
	
	public class OrderTicketOrderLine
	{
		public OrderTicketProduct Product { get; set; }
		public IEnumerable<OrderTicketTopping> Toppings { get; set; }
		public int Quantity { get; set; }

		public decimal Price { get; set; }

		public decimal OrderLineTotal { get; set; }
	}

	public class OrderTicketProduct
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
	}

	public class OrderTicketTopping
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
	}
}