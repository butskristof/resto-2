using Microsoft.Extensions.DependencyInjection;
using Resto.Common.Integrations.TicketPrinting;

namespace Resto.Infrastructure.Integrations.TicketPrinting;

public static class DependencyInjectionExtensions
{
	public static IServiceCollection AddTicketPrinting(this IServiceCollection services)
	{
		services.AddScoped<ITicketPrintingService, TicketPrintingService>();
		
		return services;
	}
}