namespace Resto.Application.Common.Contracts.Responses.Products;

public record MinimalToppingDto(
    Guid Id,
    string Name,
    decimal Price
);

public sealed record ToppingDto(
    Guid Id,
    string Name,
    decimal Price,
    DateTimeOffset? LastModifiedOn
) : MinimalToppingDto(Id, Name, Price);