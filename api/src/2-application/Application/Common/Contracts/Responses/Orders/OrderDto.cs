using Resto.Domain.Enumerations;

namespace Resto.Application.Common.Contracts.Responses.Orders;

public class OrderDto
{
    public Guid Id { get; set; }
    public OrderDiscount Discount { get; set; }
    public DateTime Timestamp { get; set; }
    public decimal OrderTotal { get; set; }

    public IEnumerable<OrderLineDto> OrderLines { get; set; }
}