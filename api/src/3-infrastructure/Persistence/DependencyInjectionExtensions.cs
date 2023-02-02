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
		var connectionString = configuration.GetConnectionString(ConfigurationConstants.ConnectionStringKey);

		services
			.AddDbContext<AppDbContext>(builder => builder
				.UseSqlServer(connectionString,
					b =>
					{
						b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
						b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
					}));

		services.AddDbContext<IAppDbContext, AppDbContext>();

		return services;
	}
}