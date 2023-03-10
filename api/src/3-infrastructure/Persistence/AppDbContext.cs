using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Resto.Application.Common.Persistence;
using Resto.Common.Constants;
using Resto.Common.Services;
using Resto.Domain.Common;
using Resto.Domain.Entities.Orders;
using Resto.Domain.Entities.Products;
using Resto.Persistence.Common;

namespace Resto.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
	#region construction

	private readonly IDateTime _dateTime;
	private readonly IMediator _mediator;

	public AppDbContext(DbContextOptions options, IDateTime dateTime, IMediator mediator) : base(options)
	{
		_dateTime = dateTime;
		_mediator = mediator;
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

		await _mediator.DispatchDomainEvents(this);
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
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}