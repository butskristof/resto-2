using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resto.Application.Common.Mapping;
using Resto.Application.Common.Persistence;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Modules.Orders;

public static class GetOrderStatistics
{
	public class Request : IRequest<Response> {}

	public class Response
	{
		public IEnumerable<ProductDto> Products { get; set; }
		
		// public class OrderStatisticsProduct
		// {
		// 	public ProductDto Product { get; set; }
		// 	public int Quantity { get; set; }
		//
		// 	public IEnumerable<OrderStatisticsTopping> Toppings { get; set; }
		// }
		//
		// public class OrderStatisticsTopping
		// {
		// 	public ToppingDto Topping { get; set; }
		// 	public int Quantity { get; set; }
		// }

		public class ProductDto : IMapFrom<Product>
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public CategoryDto Category { get; set; }
			public IEnumerable<ToppingDto> Toppings { get; set; }

			public int Quantity { get; set; }

			public void Mapping(Profile profile)
			{
				profile.CreateMap<Product, ProductDto>()
					.ForMember(dto => dto.Toppings, 
						opt => opt.Ignore());
			}
		}

		public class CategoryDto : IMapFrom<Category>
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string Color { get; set; }
		}

		public class ToppingDto : IMapFrom<Topping>
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			
			public int Quantity { get; set; }
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

			var products = await _dbContext
				.ProductsBaseQuery(false)
				.Include(p => p.Category)
				.Include(p => p.Toppings)
				.ThenInclude(pt => pt.Topping)
				.ToListAsync(cancellationToken);

			var stats = new List<Response.ProductDto>();
			foreach (var p in products)
			{
				var mapped = _mapper.Map<Response.ProductDto>(p);
				var orderLines = await GetOrderLinesForProductAsync(p, cancellationToken);
				mapped.Quantity = orderLines.Sum(ol => ol.Quantity);
				mapped.Toppings = p.Toppings
					.Select(t =>
					{
						var mappedTopping = _mapper.Map<Response.ToppingDto>(t.Topping);
						mappedTopping.Quantity = orderLines
							.Sum(ol => ol.Toppings.Count(olt => olt.ToppingId == t.ToppingId) * ol.Quantity);
						return mappedTopping;
					});
				stats.Add(mapped);
			}

			return new Response
			{
				Products = stats,
			};
		}

		private Task<List<OrderLine>> GetOrderLinesForProductAsync(Product product, 
			CancellationToken cancellationToken = default)
		{
			return _dbContext
				.Orders
				.SelectMany(o => o.OrderLines)
				.Include(ol => ol.Toppings)
				.Where(ol => ol.ProductId == product.Id)
				.ToListAsync(cancellationToken);
		}
	}
}