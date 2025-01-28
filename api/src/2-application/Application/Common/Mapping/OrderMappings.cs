using System.Linq.Expressions;
using Resto.Application.Common.Contracts.Responses.Orders;
using Resto.Application.Modules.Orders;
using Resto.Domain.Entities.Orders;

namespace Resto.Application.Common.Mapping;

internal static class OrderMappings
{
    #region CreateOrder.Request

    internal static OrderLine MapToOrderLine(this CreateOrder.Request.OrderLineRequest request)
        => new()
        {
            ProductId = request.ProductId,
            Toppings = request.ToppingIds
                .Select(id => new OrderLineTopping { ToppingId = id })
                .ToList(),
            Quantity = request.Quantity,
        };

    internal static Order MapToOrder(this CreateOrder.Request request)
        => new()
        {
            OrderLines = request.OrderLines
                .Select(ol => ol.MapToOrderLine())
                .ToList(),
            Discount = request.Discount,
        };

    #endregion

    #region OrderDto
    private static Expression<Func<Order, OrderDto>> CreateMappingExpression()
        => order => new OrderDto
        {
            Id = order.Id,
            Discount = order.Discount,
            Timestamp = order.Timestamp,
            OrderTotal = order.OrderTotal,
            OrderLines = order.OrderLines.Select(ol => new OrderLineDto
            {
                Id = ol.Id,
                Product = new OrderLineDto.OrderLineProductDto
                {
                    Id = ol.Product.Id,
                    Name = ol.Product.Name,
                    Price = ol.Product.Price,
                },
                Toppings = ol.Toppings.Select(olt => new OrderLineDto.OrderLineToppingDto
                {
                    Id = olt.Topping.Id,
                    Name = olt.Topping.Name,
                    Price = olt.Topping.Price,
                }),
                Quantity = ol.Quantity,
                Price = ol.Price,
                OrderLineTotal = ol.OrderLineTotal,
            }),
        };

    private static readonly Func<Order, OrderDto> CompiledMapping = CreateMappingExpression().Compile();

    internal static OrderDto MapToOrderDto(this Order order)
        => CompiledMapping(order);

    internal static IEnumerable<OrderDto> MapToOrderDto(this IEnumerable<Order> orders)
        => orders.Select(CompiledMapping);

    internal static IQueryable<OrderDto> MapToOrderDto(this IQueryable<Order> query)
        => query.Select(CreateMappingExpression());

    #endregion
}