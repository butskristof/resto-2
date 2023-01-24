using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Contracts.Requests.Common;
using Resto.Application.Common.Contracts.Responses.Common;
using Resto.Application.Common.Contracts.Responses.Products;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Mapping;
using Resto.Application.Common.Persistence;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Modules.Categories;

public static class GetCategories
{
	public class Request : PagedRequest, IRequest<Response>
	{
	}

	public class Response : PagedResponse<CategoryDto>, IMapFrom<PagedResponse<CategoryDto>>
	{
	}

	internal class Validator : PagedRequestValidator<Request>
	{
		public Validator() {}
	}

	internal class Handler : IRequestHandler<Request, Response>
	{
		#region construction

		private readonly ILogger<Handler> _logger;
		private readonly IAppDbContext _dbContext;
		private readonly IMapper _mapper;

		public Handler(ILogger<Handler> logger, IAppDbContext dbContext, IMapper mapper)
		{
			_logger = logger;
			_dbContext = dbContext;
			_mapper = mapper;
		}

		#endregion

		public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
		{
			_logger.LogDebug("Get categories: page {Page} w/ page size {PageSize}", 
				request.Page, request.PageSize);
			
			var categoriesQuery = _dbContext
				.Categories
				.AsNoTracking()
				.OrderBy(c => c.Name)
				.AsQueryable();

			var result = await categoriesQuery
				.GetPagedAsync<Category, CategoryDto>(_mapper, request.Page, request.PageSize,
					cancellationToken: cancellationToken);
			_logger.LogDebug("Fetched mapped categories from database");

			return _mapper.Map<Response>(result);
		}
	}
}