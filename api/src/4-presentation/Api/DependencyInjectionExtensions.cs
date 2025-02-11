using System.Text.Json.Serialization;
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
			.AddValidatedSettings<ClientSettings>(ClientSettings.SectionName)
			.AddValidatedSettings<TicketPrintingSettings>(TicketPrintingSettings.SectionName);
			
		return services;
	}

	private static IServiceCollection AddValidatedSettings<TOptions>(this IServiceCollection services,
		string sectionName)
		where TOptions : class
	{
		services
			.AddOptions<TOptions>()
			.BindConfiguration(sectionName);

		return services;
	}

	#endregion

	public static IServiceCollection AddWebApi(this IServiceCollection services)
	{
		services
			.AddCorsPolicy();

		services
			.AddControllers()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});
		
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
		var configuration = serviceProvider
			.GetRequiredService<IOptions<ClientSettings>>()
			.Value;

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