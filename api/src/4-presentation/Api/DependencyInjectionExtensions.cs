using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Resto.Common.Configuration;
using Resto.Common.Constants;
using Resto.Common.Extensions;
using Resto.Persistence;
using OpenApiConstants = Resto.Common.Constants.OpenApiConstants;

namespace Resto.Api;

public static class DependencyInjectionExtensions
{
	#region configuration

	public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.RegisterOptions<IClientConfiguration, ClientConfiguration>
				(configuration, ConfigurationConstants.Clients)
			.RegisterOptions<ITicketPrintingConfiguration, TicketPrintingConfiguration>
				(configuration, ConfigurationConstants.TicketPrinting);
		return services;
	}

	private static IServiceCollection RegisterOptions<TInterface, TImplementation>(this IServiceCollection services,
		IConfiguration configuration, string key) where TImplementation : class, TInterface
	{
		services
			.Configure<TImplementation>(configuration.GetSection(key))
			.AddSingleton(typeof(TInterface), sp =>
				sp.GetRequiredService<IOptions<TImplementation>>().Value);

		return services;
	}

	#endregion

	public static IServiceCollection AddWebApi(this IServiceCollection services)
	{
		services
			.AddCorsPolicy();

		services
			.AddControllers();
		
		services
			.AddSwaggerGen(options =>
			{
				options.SwaggerDoc(OpenApiConstants.V2, new OpenApiInfo
				{
					Title = OpenApiConstants.Title,
					Version = OpenApiConstants.V2,
				});
				options.CustomSchemaIds(type =>
				{
					var outerClass = type.ReflectedType;
					if (outerClass != null) return $"{outerClass.Name}-{type.Name}";
					if (type.Name != "Request" && type.Name != "Response") return type.Name;
					return type.GUID.ToSafeString();
				});
			});
		// TODO fluent validation in swagger
		
		services
			.AddHealthChecks()
			.AddDbContextCheck<AppDbContext>();

		return services;
	}

	private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
	{
		using var serviceProvider = services.BuildServiceProvider();
		var configuration = serviceProvider.GetRequiredService<IClientConfiguration>();
		
		services
			.AddCors(options =>
			{
				options.AddPolicy(
					ApplicationConstants.CorsPolicy,
					builder =>
					{
						builder.WithOrigins(configuration.ClientUrls);
						builder.AllowAnyHeader();
						builder.AllowAnyMethod();
					});
			});
		
		return services;
	}
}