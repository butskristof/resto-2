using Resto.Application.Modules.Orders;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Mapping;

internal static class OrderStatisticsMappings
{
    internal static GetOrderStatistics.Response.CategoryDto MapToOrderStatisticsCategoryDto(this Category category)
        => new()
        {
            Id = category.Id,
            Name = category.Name,
            Color = category.Color,
        };

    internal static GetOrderStatistics.Response.ToppingDto MapToOrderStatisticsToppingDto(this Topping topping)
        => new()
        {
            Id = topping.Id,
            Name = topping.Name,
        };

    internal static GetOrderStatistics.Response.ProductDto MapToOrderStatisticsProductDto(this Product product)
        => new()
        {
            Id = product.Id,
            Name = product.Name,
            Category = product.Category.MapToOrderStatisticsCategoryDto(),
        };
}