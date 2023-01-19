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

	public class Response : PagedResponse<FullCategoryDto>, IMapFrom<PagedResponse<FullCategoryDto>>
	{
	}

	internal class Validator : AbstractValidator<Request>
	{
		public Validator()
		{
		}
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
			_logger.LogDebug("Getting categories");

			var categoriesQuery = _dbContext
				.Categories
				.AsNoTracking()
				.OrderBy(c => c.Name)
				.AsQueryable();

			var categories = await categoriesQuery
				.GetPagedAsync<Category, FullCategoryDto>(_mapper, request.Page, request.PageSize,
					cancellationToken: cancellationToken);

			return _mapper.Map<Response>(categories);
		}
	}
}