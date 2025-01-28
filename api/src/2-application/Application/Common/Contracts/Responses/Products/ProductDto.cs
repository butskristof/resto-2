using AutoMapper;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Contracts.Responses.Products;

public class ProductDto
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