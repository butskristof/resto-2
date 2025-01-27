using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Contracts.Responses.Products;

public abstract class CategoryDtoBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
}

public class MinimalCategoryDto : CategoryDtoBase;

public class CategoryDto : CategoryDtoBase
{
    public DateTime? LastModifiedOn { get; set; }
}

internal static class MappingExtensions
{
    internal static CategoryDto MapToCategoryDto(this Category category)
        => new()
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
            LastModifiedOn = category.LastModifiedOn,
        };
}