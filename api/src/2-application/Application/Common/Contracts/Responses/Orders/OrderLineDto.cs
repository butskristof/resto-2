using AutoMapper;
using Resto.Application.Common.Mapping;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Contracts.Responses.Orders;

public class OrderLineDto : IMapFrom<OrderLine>
{
	public Guid Id { get; set; }

	public OrderLineProductDto Product { get; set; }
	public IEnumerable<OrderLineTopping> Toppings { get; set; }
	
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

	public class OrderLineTopping : IMapFrom<Topping>
	{
		public Guid Id { get; set; }
		
		public string Name { get; set; }
		public decimal Price { get; set; }
	}
}