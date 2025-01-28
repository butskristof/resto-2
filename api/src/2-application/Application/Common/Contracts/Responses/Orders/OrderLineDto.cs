namespace Resto.Application.Common.Contracts.Responses.Orders;

public class OrderLineDto
{
    public Guid Id { get; set; }

    public OrderLineProductDto Product { get; set; }
    public IEnumerable<OrderLineToppingDto> Toppings { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
    public decimal OrderLineTotal { get; set; }

    public class OrderLineProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderLineToppingDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}