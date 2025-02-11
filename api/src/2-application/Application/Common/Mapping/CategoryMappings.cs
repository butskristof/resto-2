using System.Linq.Expressions;
using Resto.Application.Common.Contracts.Responses.Products;
using Resto.Application.Modules.Categories;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Mapping;

internal static class CategoryMappings
{
    #region CreateCategory.Request

    internal static Category MapToCategory(this CreateCategory.Request request)
        => new()
        {
            Name = request.Name,
            Color = request.Color,
        };

    #endregion
    
    #region CategoryDto

    private static Expression<Func<Category, CategoryDto>> CreateMappingExpression()
        => category => new CategoryDto(
            category.Id,
            category.Name,
            category.Color,
            category.LastModifiedOn
        );

    private static readonly Func<Category, CategoryDto> CompiledMapping
        = CreateMappingExpression().Compile();

    internal static CategoryDto MapToCategoryDto(this Category category)
        => CompiledMapping(category);

    internal static IEnumerable<CategoryDto> MapToCategoryDto(this IEnumerable<Category> categories)
        => categories.Select(CompiledMapping);

    internal static IQueryable<CategoryDto> MapToCategoryDto(this IQueryable<Category> query)
        => query.Select(CreateMappingExpression());

    #endregion
}