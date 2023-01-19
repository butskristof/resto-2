using System.Reflection;
using AutoMapper;
using Resto.Application.Modules.Categories;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Mapping;

internal class MappingProfile : Profile
{
	public MappingProfile()
	{
		ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
		CreateRequestMaps();
		CreateEnumMaps();
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
		CreateMap<CreateCategory.Request, Category>()
			.IgnoreBaseEntityProperties<CreateCategory.Request, Category, Guid>()
			.IgnoreAuditableEntityProperties();
	}

	private void CreateEnumMaps()
	{
	}
}