using AutoMapper;
using Resto.Application.Common.Mapping;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Contracts.Responses.Products;

public class ProductDto : IMapFrom<Product>
{
    public Guid Id { get; set; }
    public DateTime? LastModifiedOn { get; set; }

    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool MultipleToppingsAllowed { get; set; }

    public MinimalCategoryDto Category { get; set; }
    public IEnumerable<MinimalToppingDto> Toppings { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDto>()
            .ForMember(dto => dto.Toppings, opt => opt
                .MapFrom(p => p.Toppings.Select(pt => pt.Topping)));
    }
}

internal static partial class MappingExtensions
{
    internal static ProductDto MapToProductDto(this Product product)
        => new()
        {
            Id = product.Id,
            LastModifiedOn = product.LastModifiedOn,
            Name = product.Name,
            Price = product.Price,
            MultipleToppingsAllowed = product.MultipleToppingsAllowed,
            Category = product.Category.MapToMinimalCategoryDto(),
            Toppings = product.Toppings.Select(t => t.Topping.MapToMinimalToppingDto())
        };
}