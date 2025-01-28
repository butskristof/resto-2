using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Resto.Application.Common.Persistence;
using Resto.Common.Constants;
using Resto.Domain.Common;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Entities.Products;
using Resto.Persistence.ValueConverters;

namespace Resto.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
	#region construction

	private readonly TimeProvider _timeProvider;

	public AppDbContext(DbContextOptions options, TimeProvider timeProvider) : base(options)
	{
		_timeProvider = timeProvider;
	}

	#endregion

	#region entities

	public DbSet<Category> Categories => Set<Category>();
	public DbSet<Topping> Toppings => Set<Topping>();
	public DbSet<Product> Products => Set<Product>();
	public DbSet<Order> Orders => Set<Order>();

	#endregion

	public async Task<int> SaveChangesAsync()
	{
		// change tracker
		foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
		{
			var timestamp = _timeProvider.GetLocalNow();
			switch (entry.State)
			{
				case EntityState.Added:
				{
					// entry.Entity.CreatedBy = userId;
					entry.Entity.CreatedOn = timestamp;
					break;
				}
				case EntityState.Modified:
				{
					// entry.Entity.LastModifiedBy = userId;
					entry.Entity.SetModifiedOnForContext(timestamp);
					break;
				}
			}
		}

		return await base.SaveChangesAsync();
	}

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
		base.ConfigureConventions(configurationBuilder);
		
		configurationBuilder
			.Properties<string>()
			.HaveMaxLength(ApplicationConstants.DefaultMaxStringLength);

		configurationBuilder
			.Properties<decimal>()
			.HavePrecision(18, 6);

		configurationBuilder
			.Properties<DateTimeOffset>()
			.HaveConversion<DateTimeOffsetValueConverter>();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}