using AutoMapper;

namespace Resto.Application.Common.Mapping;

internal interface IMapFrom<T>
{
	void Mapping(Profile profile)
	{
		profile.CreateMap(typeof(T), GetType());
	}
}