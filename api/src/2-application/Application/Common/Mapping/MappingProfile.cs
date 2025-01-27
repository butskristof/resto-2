using System.Reflection;
using AutoMapper;
using Resto.Application.Modules.Categories;
using Resto.Application.Modules.Orders;
using Resto.Application.Modules.Products;
using Resto.Application.Modules.Toppings;
using Resto.Common.Integrations.TicketPrinting.Models;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Mapping;

internal class MappingProfile : Profile
{
	public MappingProfile()
	{
		ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
		CreateRequestMaps();
		CreateAdditionalMaps();
	}

	private void ApplyMappingsFromAssembly(Assembly assembly)
	{
		var types = assembly.GetExportedTypes()
			.Where(t => t.GetInterfaces().Any(i =>
				i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
			.ToList();

		foreach (var type in types)
		{
			var instance = Activator.CreateInstance(type);

			var methodInfo = type.GetMethod("Mapping")
			                 ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

			methodInfo?.Invoke(instance, new object[] {this});
		}
	}

	private void CreateRequestMaps()
	{
		CreateMap<CreateProduct.Request, Product>()
			.IgnoreBaseEntityProperties<CreateProduct.Request, Product, Guid>()
			.IgnoreAuditableEntityProperties()
			.ForMember(p => p.Toppings, opt => opt
				.MapFrom(r => r.ToppingIds.Select(tid => new ProductTopping { ToppingId = tid })));
		CreateMap<UpdateProduct.Request, Product>()
			.IgnoreBaseEntityProperties<UpdateProduct.Request, Product, Guid>()
			.IgnoreAuditableEntityProperties(false);

		CreateMap<CreateOrder.Request, Order>()
			.IgnoreBaseEntityProperties<CreateOrder.Request, Order, Guid>()
			.IgnoreAuditableEntityProperties()
			.ForMember(o => o.Timestamp, opt => opt.Ignore());
		CreateMap<CreateOrder.Request.OrderLineRequest, OrderLine>()
			.IgnoreBaseEntityProperties<CreateOrder.Request.OrderLineRequest, OrderLine, Guid>()
			.IgnoreAuditableEntityProperties()
			.ForMember(ol => ol.Toppings, opt => opt
				.MapFrom(olr => olr.ToppingIds.Select(tid => new OrderLineTopping { ToppingId = tid})));
	}

	private void CreateAdditionalMaps()
	{
		CreateMap<Order, OrderTicketData>();
		CreateMap<OrderLine, OrderTicketData.OrderTicketOrderLine>()
			.ForMember(dest => dest.Toppings, opt => opt
				.MapFrom(src => src.Toppings.Select(olt => olt.Topping)));
		CreateMap<Product, OrderTicketData.OrderTicketProduct>();
		CreateMap<Topping, OrderTicketData.OrderTicketTopping>();
	}
}