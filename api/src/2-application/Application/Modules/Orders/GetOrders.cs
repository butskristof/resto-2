using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Contracts.Requests.Common;
using Resto.Application.Common.Contracts.Responses.Common;
using Resto.Application.Common.Contracts.Responses.Orders;
using Resto.Application.Common.Extensions;
using Resto.Application.Common.Mapping;
using Resto.Application.Common.Persistence;
using Resto.Domain.Entities.Orders;

namespace Resto.Application.Modules.Orders;

public static class GetOrders
{
	public class Request : PagedRequest, IRequest<Response>
	{
	}

	public class Response : PagedResponse<OrderDto>, IMapFrom<PagedResponse<OrderDto>>
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
			_logger.LogDebug("Get orders: page {Page} w/ page size {PageSize}",
				request.Page, request.PageSize);

			var ordersQuery = _dbContext
				.OrdersBaseQuery(false);

#pragma warning disable CS0618 // Type or member is obsolete
			var result = await ordersQuery
				.GetPagedAsync<Order, OrderDto>(_mapper, request.Page, request.PageSize,
					cancellationToken: cancellationToken);
#pragma warning restore CS0618 // Type or member is obsolete
			_logger.LogDebug("Fetched mapped orders from database");
			
			return _mapper.Map<Response>(result);
		}
	}
}	