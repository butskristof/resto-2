using Resto.Application.Common.Mapping;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Contracts.Responses.Products;

public abstract class ToppingDtoBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class MinimalToppingDto : ToppingDtoBase, IMapFrom<Topping>
{
}

public class ToppingDto : ToppingDtoBase, IMapFrom<Topping>
{
    public DateTime? LastModifiedOn { get; set; }
}

internal static partial class MappingExtensions
{
    internal static MinimalToppingDto MapToMinimalToppingDto(this Topping topping)
        => new()
        {
            Id = topping.Id,
            Name = topping.Name,
            Price = topping.Price,
        };

    internal static ToppingDto MapToToppingDto(this Topping topping)
        => new()
        {
            Id = topping.Id,
            Name = topping.Name,
            Price = topping.Price,
            LastModifiedOn = topping.LastModifiedOn,
        };
}