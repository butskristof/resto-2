namespace Resto.Application.Common.Contracts.Responses.Products;

public record MinimalCategoryDto(
    Guid Id,
    string Name,
    string Color
);

public sealed record CategoryDto(
    Guid Id,
    string Name,
    string Color,
    DateTimeOffset? LastModifiedOn
) : MinimalCategoryDto(Id, Name, Color);