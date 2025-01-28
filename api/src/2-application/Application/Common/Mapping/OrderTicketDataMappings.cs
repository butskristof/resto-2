using Resto.Common.Integrations.TicketPrinting.Models;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Entities.Products;
using Resto.Domain.Enumerations;

namespace Resto.Application.Common.Mapping;

internal static class OrderTicketDataMappings
{
    internal static OrderTicketData.OrderTicketTopping MapToOrderTicketTopping(this Topping topping)
        => new()
        {
            Name = topping.Name,
            Price = topping.Price,
        };

    internal static OrderTicketData.OrderTicketProduct MapToOrderTicketProduct(this Product product)
        => new()
        {
            Name = product.Name,
            Price = product.Price,
        };

    internal static OrderTicketData.OrderTicketOrderLine MapToOrderTicketOrderLine(this OrderLine orderLine)
        => new()
        {
            Product = orderLine.Product.MapToOrderTicketProduct(),
            Toppings = orderLine.Toppings
                .Select(t => t.Topping.MapToOrderTicketTopping()),
            Quantity = orderLine.Quantity,
            Price = orderLine.Price,
            OrderLineTotal = orderLine.OrderLineTotal,
        };

    internal static OrderTicketData.OrderTicketDiscount MapToOrderTicketDiscount(this OrderDiscount orderDiscount)
        => (OrderTicketData.OrderTicketDiscount)orderDiscount;

    internal static OrderTicketData MapToOrderTicketData(this Order order)
        => new()
        {
            Id = order.Id,
            Timestamp = order.Timestamp,
            Discount = order.Discount.MapToOrderTicketDiscount(),
            OrderLines = order.OrderLines
                .Select(ol => ol.MapToOrderTicketOrderLine()),
            OrderTotal = order.OrderTotal,
        };
}