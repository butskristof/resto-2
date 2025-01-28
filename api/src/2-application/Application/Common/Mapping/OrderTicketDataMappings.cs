using System.Linq.Expressions;
using Resto.Common.Integrations.TicketPrinting.Models;
using Resto.Domain.Entities.Orders;

namespace Resto.Application.Common.Mapping;

internal static class OrderTicketDataMappings
{
    private static Expression<Func<Order, OrderTicketData>> CreateMappingExpression()
        => order => new OrderTicketData
        {
            Id = order.Id,
            Timestamp = order.Timestamp,
            Discount = (OrderTicketData.OrderTicketDiscount)order.Discount,
            OrderLines = order.OrderLines
                .Select(ol => new OrderTicketData.OrderTicketOrderLine
                {
                    Product = new OrderTicketData.OrderTicketProduct
                    {
                        Name = ol.Product.Name,
                        Price = ol.Product.Price,
                    },
                    Toppings = ol.Toppings
                        .Select(olt => new OrderTicketData.OrderTicketTopping
                        {
                            Name = olt.Topping.Name,
                            Price = olt.Topping.Price,
                        }),
                    Quantity = ol.Quantity,
                    Price = ol.Price,
                    OrderLineTotal = ol.OrderLineTotal,
                }),
            OrderTotal = order.OrderTotal,
        };

    private static readonly Func<Order, OrderTicketData> CompiledMapping = CreateMappingExpression().Compile();

    internal static OrderTicketData MapToOrderTicketData(this Order order) => CompiledMapping(order);

    internal static IEnumerable<OrderTicketData> MapToOrderTicketData(this IEnumerable<Order> orders)
        => orders.Select(CompiledMapping);

    internal static IQueryable<OrderTicketData> MapToOrderTicketData(this IQueryable<Order> query)
        => query.Select(CreateMappingExpression());
}