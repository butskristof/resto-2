using System.Linq.Expressions;
using Resto.Application.Common.Contracts.Responses.Orders;
using Resto.Application.Modules.Orders;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Entities.Products;

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

    internal static OrderLineDto.OrderLineProductDto MapToOrderLineProductDto(this Product product)
        => new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
        };

    internal static OrderLineDto.OrderLineToppingDto MapToOrderLineToppingDto(this Topping topping)
        => new()
        {
            Id = topping.Id,
            Name = topping.Name,
            Price = topping.Price,
        };

    internal static OrderLineDto MapToOrderLineDto(this OrderLine orderLine)
        => new()
        {
            Id = orderLine.Id,
            Product = orderLine.Product.MapToOrderLineProductDto(),
            Toppings = orderLine.Toppings.Select(t => t.Topping.MapToOrderLineToppingDto()),
            Quantity = orderLine.Quantity,
            Price = orderLine.Price,
            OrderLineTotal = orderLine.OrderLineTotal,
        };

    private static Expression<Func<Order, OrderDto>> CreateMappingExpression()
        => order => new OrderDto
        {
            Id = order.Id,
            Discount = order.Discount,
            Timestamp = order.Timestamp,
            OrderTotal = order.OrderTotal,
            OrderLines = order.OrderLines.Select(ol => ol.MapToOrderLineDto()),
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