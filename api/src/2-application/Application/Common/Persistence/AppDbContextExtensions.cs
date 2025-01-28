using Microsoft.EntityFrameworkCore;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Persistence;

internal static class AppDbContextExtensions
{
    internal static IQueryable<Order> OrdersBaseQuery(this IAppDbContext context, bool tracking = true)
    {
        var query = context
            .Orders
            .OrderByDescending(o => o.Timestamp)
            .AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();

        query = query
            .Include(o => o.OrderLines)
            .ThenInclude(ol => ol.Product)
            .Include(o => o.OrderLines)
            .ThenInclude(ol => ol.Toppings)
            .ThenInclude(olt => olt.Topping);

        return query;
    }

    internal static IQueryable<Product> ProductsBaseQuery(this IAppDbContext context, bool tracking = true,
        bool includeCategory = false, bool includeToppings = false)
    {
        var query = context
            .Products
            .OrderBy(p => p.Category.Name)
            .ThenBy(p => p.LastModifiedOn)
            .AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();
        if (includeCategory)
            query = query.Include(p => p.Category);
        if (includeToppings)
            query = query
                .Include(p => p.Toppings)
                .ThenInclude(pt => pt.Topping);

        return query;
    }
}