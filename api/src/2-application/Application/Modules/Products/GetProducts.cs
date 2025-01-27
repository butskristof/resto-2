using MediatR;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Contracts.Requests.Common;
using Resto.Application.Common.Contracts.Responses.Common;
using Resto.Application.Common.Contracts.Responses.Products;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Mapping;
using Resto.Application.Common.Persistence;

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

		public Handler(ILogger<Handler> logger, IAppDbContext dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		#endregion

		public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
		{
			_logger.LogDebug("Get products: page {Page} w/ page size {PageSize}",
				request.Page, request.PageSize);

			var productsQuery = _dbContext
				.ProductsBaseQuery(false, true, true);

			var result = await productsQuery
				.GetPagedAsync(p => p.MapToProductDto(), request.Page, request.PageSize,
					cancellationToken: cancellationToken);
			_logger.LogDebug("Fetched mapped products from database");

			return result.MapToTypedResponse<ProductDto, Response>();
		}
	}
}