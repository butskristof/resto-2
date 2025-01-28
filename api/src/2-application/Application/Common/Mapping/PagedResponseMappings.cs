using Resto.Application.Common.Contracts.Responses.Common;

namespace Resto.Application.Common.Mapping;

internal static class PagedResponseMappings
{
    internal static TResponse MapToTypedResponse<TDto, TResponse>(this PagedResponse<TDto> source)
        where TDto : class
        where TResponse : PagedResponse<TDto>, new()
        => new()
        {
            CurrentPage = source.CurrentPage,
            PageCount = source.PageCount,
            PageSize = source.PageSize,
            RowCount = source.RowCount,
            Results = source.Results,
        };
}