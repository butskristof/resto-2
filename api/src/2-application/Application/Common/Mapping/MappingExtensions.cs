using AutoMapper;
using Resto.Domain.Common;

namespace Resto.Application.Common.Mapping;
internal static class MappingExtensions
{
	internal static IMappingExpression<TSource, TDestination>
		IgnoreBaseEntityProperties<TSource, TDestination, TDestinationId>(
			this IMappingExpression<TSource, TDestination> expression,
			bool ignoreLastModifiedOn = true)
		where TDestination : BaseEntity<TDestinationId>
	{
		expression
			.ForMember(e => e.Id, opt => opt.Ignore());

		return expression;
	}

	internal static IMappingExpression<TSource, TDestination> IgnoreAuditableEntityProperties<TSource, TDestination>(
		this IMappingExpression<TSource, TDestination> expression, bool ignoreLastModifiedOn = true)
		where TDestination : IAuditableEntity
	{
		expression
			// .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
			.ForMember(dest => dest.CreatedOn, opt => opt.Ignore());
			// .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
		
		if (ignoreLastModifiedOn)
		{
			expression
				.ForMember(e => e.LastModifiedOn, opt => opt.Ignore());
		}

		return expression;
	}
}
