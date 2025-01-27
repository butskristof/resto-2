using AutoMapper;
using Resto.Application.Common.Mapping;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Contracts.Responses.Orders;

public class OrderLineDto : IMapFrom<OrderLine>
{
    public Guid Id { get; set; }

    public OrderLineProductDto Product { get; set; }
    public IEnumerable<OrderLineToppingDto> Toppings { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
    public decimal OrderLineTotal { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderLine, OrderLineDto>()
            .ForMember(dto => dto.Toppings, opt => opt
                .MapFrom(ol => ol.Toppings.Select(olt => olt.Topping)));
    }

    public class OrderLineProductDto : IMapFrom<Product>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderLineToppingDto : IMapFrom<Topping>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}

internal static partial class MappingExtensions
{
    internal static OrderLineDto.OrderLineProductDto MapToOrderLineProductDto(this Product product)
        => new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
        };

    internal static OrderLineDto.OrderLineToppingDto MapToOrderLineToppingDto(this Topping topping)
        => new()
        {
            Id = topping.Id,
            Name = topping.Name,
            Price = topping.Price,
        };

    internal static OrderLineDto MapToOrderLineDto(this OrderLine orderLine)
        => new()
        {
            Id = orderLine.Id,
            Product = orderLine.Product.MapToOrderLineProductDto(),
            Toppings = orderLine.Toppings.Select(t => t.Topping.MapToOrderLineToppingDto()),
            Quantity = orderLine.Quantity,
            Price = orderLine.Price,
            OrderLineTotal = orderLine.OrderLineTotal,
        };
}