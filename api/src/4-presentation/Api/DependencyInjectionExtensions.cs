using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Resto.Common.Configuration;
using Resto.Common.Constants;
using Resto.Persistence;
using OpenApiConstants = Resto.Common.Constants.OpenApiConstants;

namespace Resto.Api;

public static class DependencyInjectionExtensions
{
	public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddCorsPolicies(configuration);

		services
			.AddControllers();

		services
			.AddSwaggerGen(options => options.SwaggerDoc(OpenApiConstants.V2, new OpenApiInfo
			{
				Title = OpenApiConstants.Title,
				Version = OpenApiConstants.V2,
			}));

		services
			.AddHealthChecks()
			.AddDbContextCheck<AppDbContext>();
		
		return services;
	}

	#region configuration

	private static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.RegisterOptions<IClientOptions, ClientOptions>
				(configuration, ConfigurationConstants.Clients);
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
	
	private static IServiceCollection AddCorsPolicies(this IServiceCollection services, IConfiguration configuration)
	{
		var clientOptions = configuration
			.GetSection(ConfigurationConstants.Clients)
			.Get<ClientOptions>();

		services
			.AddCors(options =>
			{
				options.AddPolicy(
					PolicyConstants.CorsClients,
					builder =>
					{
						builder.WithOrigins(clientOptions.ClientUrls);
						builder.AllowAnyHeader();
						builder.AllowAnyMethod();
					});
			});

		return services;
	}
}