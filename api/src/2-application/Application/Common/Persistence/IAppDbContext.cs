using Microsoft.EntityFrameworkCore;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Persistence;

public interface IAppDbContext
{
	// DbSet<Product> Products { get; }
	DbSet<Category> Categories { get; }
	DbSet<Topping> Toppings { get; }

	Task<int> SaveChangesAsync();
}