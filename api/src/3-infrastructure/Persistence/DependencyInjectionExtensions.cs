using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resto.Application.Common.Persistence;
using Resto.Common.Constants;

namespace Resto.Persistence;

public static class DependencyInjectionExtensions
{
	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString =
			configuration.GetConnectionString(ConfigurationConstants.AppDbContextConnectionStringKey);

		services
			.AddDbContext<AppDbContext>(options => options
				.UseSqlServer(connectionString, builder =>
				{
					builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
					builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
				}));
		
		services.AddScoped<IAppDbContext, AppDbContext>();
		
		return services;
	}
}