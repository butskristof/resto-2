namespace Resto.Application.Common.Contracts.Responses.Products;

public sealed record ProductDto(
    Guid Id,
    string Name,
    decimal Price,
    bool MultipleToppingsAllowed,
    MinimalCategoryDto Category,
    IEnumerable<MinimalToppingDto> Toppings,
    DateTimeOffset? LastModifiedOn
);