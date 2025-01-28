using System.Linq.Expressions;
using Resto.Application.Common.Contracts.Responses.Products;
using Resto.Application.Modules.Toppings;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Mapping;

internal static class ToppingMappings
{
    #region CreateTopping.Request

    internal static Topping MapToTopping(this CreateTopping.Request request)
        => new()
        {
            Name = request.Name,
            Price = request.Price,
        };

    #endregion

    #region ToppingDto

    private static Expression<Func<Topping, ToppingDto>> CreateMappingExpression()
        => topping => new ToppingDto
        {
            Id = topping.Id,
            Name = topping.Name,
            Price = topping.Price,
            LastModifiedOn = topping.LastModifiedOn,
        };

    private static readonly Func<Topping, ToppingDto> CompiledMapping = CreateMappingExpression().Compile();

    internal static ToppingDto MapToToppingDto(this Topping topping)
        => CompiledMapping(topping);

    internal static IEnumerable<ToppingDto> MapToToppingDto(this IEnumerable<Topping> toppings)
        => toppings.Select(CompiledMapping);

    internal static IQueryable<ToppingDto> MapToToppingDto(this IQueryable<Topping> query)
        => query.Select(CreateMappingExpression());

    #endregion
}