using Microsoft.EntityFrameworkCore;
using Resto.Domain.Entities.Orders;

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
}