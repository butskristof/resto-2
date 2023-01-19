using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Resto.Application.Common.Contracts.Responses.Common;

namespace Resto.Application.Common.Extensions;

internal static class PaginationExtensions
{
	public static async Task<PagedResponse<T>> GetPagedAsync<T>(this IQueryable<T> query,
		int page, int pageSize, CancellationToken cancellationToken = default) 
		where T : class
	{
		PagedResponse<T> result;
		(result, query) = await GetPagedBaseAsync<T, T>(query, page, pageSize, cancellationToken);
		result.Results = await query
			.ToListAsync(cancellationToken);

		return result;
	}

	public static async Task<PagedResponse<TDestination>> GetPagedAsync<TSource, TDestination>(
		this IQueryable<TSource> query,
		IMapper mapper,
		int page,
		int pageSize,
		object parameters = null, // for parameterization in projections (https://docs.automapper.org/en/latest/Queryable-Extensions.html#parameterization)
		CancellationToken cancellationToken = default)
		where TDestination : class
	{
		PagedResponse<TDestination> result;
		(result, query) = await GetPagedBaseAsync<TSource, TDestination>(query, page, pageSize, cancellationToken);
		result.Results = await query
			.ProjectTo<TDestination>(mapper.ConfigurationProvider, parameters)
			.ToListAsync(cancellationToken);

		return result;
	}

	private static async Task<(PagedResponse<TDestination>, IQueryable<TSource>)>
		GetPagedBaseAsync<TSource, TDestination>(
			this IQueryable<TSource> query,
			int page,
			int pageSize,
			CancellationToken cancellationToken = default)
		where TDestination : class
	{
		if (page < 1) throw new ArgumentException("Page must be at least 1.");
		if (pageSize < 1) throw new ArgumentException("Page size must be at least 1.");

		var result = new PagedResponse<TDestination>
		{
			CurrentPage = page,
			PageSize = pageSize,
			RowCount = await query.CountAsync(cancellationToken)
		};

		var pageCount = (double) result.RowCount / pageSize;
		result.PageCount = (int) Math.Ceiling(pageCount);

		var skip = (page - 1) * pageSize;
		query = query
			.Skip(skip)
			.Take(pageSize);

		return (result, query);
	}

	public static PagedResponse<T> GetPaged<T>(this IQueryable<T> query,
		int page, int pageSize) where T : class
	{
		PagedResponse<T> result;
		(result, query) = GetPagedBase<T, T>(query, page, pageSize);
		result.Results = query.ToList();

		return result;
	}

	public static PagedResponse<TDestination> GetPaged<TSource, TDestination>(
		this IQueryable<TSource> query,
		IMapper mapper,
		int page,
		int pageSize,
		object parameters = null) // for parameterization in projections (https://docs.automapper.org/en/latest/Queryable-Extensions.html#parameterization)
		where TDestination : class
	{
		PagedResponse<TDestination> result;
		(result, query) = GetPagedBase<TSource, TDestination>(query, page, pageSize);
		result.Results = query
			.ProjectTo<TDestination>(mapper.ConfigurationProvider, parameters)
			.ToList();

		return result;
	}

	private static (PagedResponse<TDestination>, IQueryable<TSource>)
		GetPagedBase<TSource, TDestination>(
			this IQueryable<TSource> query,
			int page,
			int pageSize)
		where TDestination : class
	{
		if (page < 1) throw new ArgumentException("Page must be at least 1.");
		if (pageSize < 1) throw new ArgumentException("Page size must be at least 1.");

		var result = new PagedResponse<TDestination>
		{
			CurrentPage = page,
			PageSize = pageSize,
			RowCount = query.Count()
		};

		var pageCount = (double) result.RowCount / pageSize;
		result.PageCount = (int) Math.Ceiling(pageCount);

		var skip = (page - 1) * pageSize;
		query = query
			.Skip(skip)
			.Take(pageSize);

		return (result, query);
	}
}