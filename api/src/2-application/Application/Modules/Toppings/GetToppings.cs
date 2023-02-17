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

namespace Resto.Application.Modules.Toppings;

public static class GetToppings
{
	public class Request : PagedRequest, IRequest<Response> {}
	public class Response : PagedResponse<ToppingDto>, IMapFrom<PagedResponse<ToppingDto>> {}
	
	internal class Validator : PagedRequestValidator<Request> {}

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
			_logger.LogDebug("Get toppings: page {Page} w/ page size {PageSize}",
				request.Page, request.PageSize);

			var toppingsQuery = _dbContext
				.Toppings
				.AsNoTracking()
				.OrderBy(e => e.Name)
				.AsQueryable();
			var result = await toppingsQuery
				.GetPagedAsync<Topping, ToppingDto>(_mapper, request.Page, request.PageSize, 
					cancellationToken: cancellationToken);
			_logger.LogDebug("Fetched mapped toppings from database");

			return _mapper.Map<Response>(result);
		}
	}
}