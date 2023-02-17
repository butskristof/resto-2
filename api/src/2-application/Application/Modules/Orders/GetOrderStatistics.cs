using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Mapping;
using Resto.Application.Common.Persistence;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Modules.Orders;

public static class GetOrderStatistics
{
	public class Request : IRequest<Response> {}

	public class Response
	{
		public IEnumerable<OrderStatisticsProduct> Products { get; set; }
		
		public class OrderStatisticsProduct
		{
			public ProductDto Product { get; set; }
			public int Quantity { get; set; }

			public IEnumerable<OrderStatisticsTopping> Toppings { get; set; }
		}

		public class OrderStatisticsTopping
		{
			public ToppingDto Topping { get; set; }
			public int Quantity { get; set; }
		}

		public class ProductDto : IMapFrom<Product>
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
		}

		public class ToppingDto : IMapFrom<Topping>
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
		}
	}
	
	internal class Validator : AbstractValidator<Request> {}

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
			_logger.LogDebug("Calculating order statistics");

			var groupedOrderLines = await _dbContext
				.OrdersBaseQuery(false)
				.SelectMany(o => o.OrderLines)
				.GroupBy(ol => ol.Product)
				.ToListAsync(cancellationToken);

			var stats = groupedOrderLines
				.Select(g =>
				{
					var toppings = g
						.SelectMany(ol => ol.Toppings)
						.GroupBy(olt => olt.ToppingId)
						.Select(gg => new Response.OrderStatisticsTopping
						{
							Topping = _mapper.Map<Response.ToppingDto>(gg.First().Topping),
							Quantity = gg.Count(),
						});
					
					var result = new Response.OrderStatisticsProduct
					{
						Product = _mapper.Map<Response.ProductDto>(g.Key),
						Quantity = g.Sum(ol => ol.Quantity),
						Toppings = toppings,
					};
					return result;
				})
				.ToList();

			return new Response
			{
				Products = stats,
			};
		}
	}
}