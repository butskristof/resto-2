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
    public class Request : IRequest<Response>;

    public class Response
    {
        public int OrderCount { get; set; }
        public IEnumerable<ProductDto> ProductStatistics { get; set; }
        public IEnumerable<OrderCountDto> OrderStatistics { get; set; }

        public class OrderCountDto
        {
            public DateTime Timestamp { get; set; }
            public DateTime Date { get; set; }
            public int Hour { get; set; }
            public int Count { get; set; }
        }

        public class ProductDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public CategoryDto Category { get; set; }
            public IEnumerable<ToppingDto> Toppings { get; set; }

            public int Quantity { get; set; }
        }

        public class CategoryDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
        }

        public class ToppingDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public int Quantity { get; set; }
        }
    }

    internal class Validator : AbstractValidator<Request>
    {
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
            _logger.LogDebug("Calculating order statistics");

            return new Response
            {
                OrderCount = await _dbContext.OrdersBaseQuery(false).CountAsync(cancellationToken),
                OrderStatistics = await GetOrderStatistics(cancellationToken),
                ProductStatistics = await GetProductStatistics(cancellationToken),
            };
        }

        private async Task<List<Response.OrderCountDto>> GetOrderStatistics(CancellationToken cancellationToken)
        {
            var orders = await _dbContext
                .Orders
                .AsNoTracking()
                .GroupBy(o => new { o.Timestamp.Date, o.Timestamp.Hour })
                .Select(g => new Response.OrderCountDto()
                {
                    Date = g.Key.Date,
                    Hour = g.Key.Hour,
                    Timestamp = new DateTime(g.Key.Date.Year, g.Key.Date.Month, g.Key.Date.Day, g.Key.Hour, 0, 0),
                    Count = g.Count(),
                })
                .OrderBy(o => o.Date)
                .ThenBy(o => o.Hour)
                .ToListAsync(cancellationToken);
            return orders;
            // .Select(o => new coun)
        }

        private async Task<List<Response.ProductDto>> GetProductStatistics(CancellationToken cancellationToken)
        {
            var products = await _dbContext
                .ProductsBaseQuery(false)
                .Include(p => p.Category)
                .Include(p => p.Toppings)
                .ThenInclude(pt => pt.Topping)
                .ToListAsync(cancellationToken);

            var stats = new List<Response.ProductDto>();
            foreach (var p in products)
            {
                var mapped = p.MapToOrderStatisticsProductDto();
                var orderLines = await GetOrderLinesForProductAsync(p, cancellationToken);
                mapped.Quantity = orderLines.Sum(ol => ol.Quantity);
                mapped.Toppings = p.Toppings
                    .Select(t =>
                    {
                        var mappedTopping = t.Topping.MapToOrderStatisticsToppingDto();
                        mappedTopping.Quantity = orderLines
                            .Sum(ol => ol.Toppings.Count(olt => olt.ToppingId == t.ToppingId) * ol.Quantity);
                        return mappedTopping;
                    });
                stats.Add(mapped);
            }

            _logger.LogDebug("Fetched product statistics from database and mapped to DTOs");

            return stats;
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
