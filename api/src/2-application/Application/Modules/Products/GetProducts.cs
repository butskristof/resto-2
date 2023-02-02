using AutoMapper;
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

namespace Resto.Application.Modules.Products;

public static class GetProducts
{
	public class Request : PagedRequest, IRequest<Response> {}

	public class Response : PagedResponse<ProductDto>, IMapFrom<PagedResponse<ProductDto>>
	{
	}

	internal class Validator : PagedRequestValidator<Request>
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
			_logger.LogDebug("Get products: page {Page} w/ page size {PageSize}",
				request.Page, request.PageSize);

			var productsQuery = _dbContext
				.Products
				.AsNoTracking()
				.OrderByDescending(p => p.LastModifiedOn)
				.AsQueryable();

			var result = await productsQuery
				.GetPagedAsync<Product, ProductDto>(_mapper, request.Page, request.PageSize,
					cancellationToken: cancellationToken);
			_logger.LogDebug("Fetched mapped products from database");

			return _mapper.Map<Response>(result);

		}
	}
}