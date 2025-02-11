namespace Resto.Application.Common.Contracts.Responses.Orders;

public sealed record OrderLineDto(
    Guid Id,
    OrderLineDto.OrderLineProductDto Product,
    IEnumerable<OrderLineDto.OrderLineToppingDto> Toppings,
    int Quantity,
    decimal Price,
    decimal OrderLineTotal
)
{
    public sealed record OrderLineProductDto(
        Guid Id,
        string Name,
        decimal Price
    );

    public sealed record OrderLineToppingDto(
        Guid Id,
        string Name,
        decimal Price
    );
}