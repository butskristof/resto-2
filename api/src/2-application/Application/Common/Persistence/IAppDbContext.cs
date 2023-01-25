using Microsoft.EntityFrameworkCore;
using Resto.Domain.Entities.Products;

namespace Resto.Application.Common.Persistence;

public interface IAppDbContext
{
	DbSet<Category> Categories { get; }
	DbSet<Topping> Toppings { get; }
	DbSet<Product> Products { get; }

	Task<int> SaveChangesAsync();
}