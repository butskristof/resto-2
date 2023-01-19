using AutoMapper;
using Resto.Domain.Common;

namespace Resto.Application.Common.Mapping;

internal static class MappingExtensions
{
	internal static IMappingExpression<TSource, TDestination>
		IgnoreEntityBaseProperties<TSource, TDestination>(
			this IMappingExpression<TSource, TDestination> expression,
			bool ignoreLastModifiedOn = true)
		where TDestination : BaseEntity
	{
		expression
			.ForMember(e => e.Id, opt => opt.Ignore())
			// .ForMember(e => e.CreatedBy, opt => opt.Ignore())
			.ForMember(e => e.CreatedOn, opt => opt.Ignore());
			// .ForMember(e => e.LastModifiedBy, opt => opt.Ignore());
			
		if (ignoreLastModifiedOn)
		{
			expression
				.ForMember(e => e.LastModifiedOn, opt => opt.Ignore());
		}

		return expression;
	}
}