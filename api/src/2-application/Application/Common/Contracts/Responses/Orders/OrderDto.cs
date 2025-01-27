using Resto.Application.Common.Mapping;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Enumerations;

namespace Resto.Application.Common.Contracts.Responses.Orders;

public class OrderDto : IMapFrom<Order>
{
    public Guid Id { get; set; }
    public OrderDiscount Discount { get; set; }
    public DateTime Timestamp { get; set; }
    public decimal OrderTotal { get; set; }

    public IEnumerable<OrderLineDto> OrderLines { get; set; }
}

internal static partial class MappingExtensions
{
    internal static OrderDto MapToOrderDto(this Order order)
        => new()
        {
            Id = order.Id,
            Discount = order.Discount,
            Timestamp = order.Timestamp,
            OrderTotal = order.OrderTotal,
            OrderLines = order.OrderLines.Select(ol => ol.MapToOrderLineDto()),
        };
}