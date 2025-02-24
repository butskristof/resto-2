using Microsoft.Extensions.DependencyInjection;
using Resto.Common.Services;
using Resto.Infrastructure.Integrations.TicketPrinting;
using Resto.Infrastructure.Services;

namespace Resto.Infrastructure;

public static class DependencyInjectionExtensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services
			.AddTicketPrinting();

		services.AddSingleton(TimeProvider.System);
		services.AddTransient<IGuid, GuidService>();

		return services;
	}
}