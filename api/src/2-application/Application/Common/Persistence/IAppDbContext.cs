using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Persistence;

public interface IAppDbContext
{
	DbSet<Category> Categories { get; }
	DbSet<Topping> Toppings { get; }
	DbSet<Product> Products { get; }
	DbSet<Order> Orders { get; }

	Task<int> SaveChangesAsync();
	EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}