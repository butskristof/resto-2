using System.Linq.Expressions;
using Resto.Application.Common.Contracts.Responses.Products;
using Resto.Application.Modules.Products;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Mapping;

internal static class ProductMappings
{
    #region CreateProduct.Request

    internal static Product MapToProduct(this CreateProduct.Request request)
        => new()
        {
            Name = request.Name,
            Price = request.Price,
            MultipleToppingsAllowed = request.MultipleToppingsAllowed,
            CategoryId = request.CategoryId,
            Toppings = request.ToppingIds
                           .Select(toppingId => new ProductTopping { ToppingId = toppingId })
                .ToList(),
        };

    #endregion

    #region ProductDto

    private static Expression<Func<Product, ProductDto>> CreateMappingExpression()
        => product => new ProductDto
        (
            product.Id,
            product.Name,
            product.Price,
            product.MultipleToppingsAllowed,
            new MinimalCategoryDto(
                product.Category.Id,
                product.Category.Name,
                product.Category.Color
            ),
            product.Toppings.Select(t => new MinimalToppingDto(
                t.Topping.Id,
                t.Topping.Name,
                t.Topping.Price
            )),
            product.LastModifiedOn
        );

    private static readonly Func<Product, ProductDto> CompiledMapping = CreateMappingExpression().Compile();

    internal static ProductDto MapToProductDto(this Product product)
        => CompiledMapping(product);

    internal static IEnumerable<ProductDto> MapToProductDto(this IEnumerable<Product> products)
        => products.Select(CompiledMapping);

    internal static IQueryable<ProductDto> MapToProductDto(this IQueryable<Product> query)
        => query.Select(CreateMappingExpression());

    #endregion
}