using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Resto.Application.Common.Persistence;
using Resto.Common.Constants;
using Resto.Common.Services;
using Resto.Domain.Common;
using Resto.Domain.Entities.Products;

namespace Resto.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
	#region construction

	private readonly IDateTime _dateTime;

	public AppDbContext(DbContextOptions options, IDateTime dateTime) : base(options)
	{
		_dateTime = dateTime;
	}

	#endregion

	#region entities

	public DbSet<Category> Categories => Set<Category>();
	public DbSet<Topping> Toppings => Set<Topping>();

	#endregion

	public Task<int> SaveChangesAsync()
	{
		// change tracker
		foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
		{
			var timestamp = _dateTime.Now;
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

		return base.SaveChangesAsync();
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
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}