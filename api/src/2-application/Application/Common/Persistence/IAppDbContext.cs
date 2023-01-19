using Microsoft.EntityFrameworkCore;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Persistence;

public interface IAppDbContext
{
	// DbSet<Product> Products { get; }
	// DbSet<Topping> Toppings { get; }
	DbSet<Category> Categories { get; }

	Task<int> SaveChangesAsync();
}