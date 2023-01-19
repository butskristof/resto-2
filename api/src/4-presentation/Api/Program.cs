using Microsoft.EntityFrameworkCore;
using Resto.Api;
using Resto.Api.Middlewares;
using Resto.Application;
using Resto.Common.Constants;
using Resto.Infrastructure;
using Resto.Persistence;
using Serilog;
using Serilog.Exceptions;

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.CreateBootstrapLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((ctx, config) =>
{
	config
		.Enrich.WithExceptionDetails()
		.ReadFrom.Configuration(ctx.Configuration);
});

builder.Services
	.AddApplication()
	.AddInfrastructure()
	.AddPersistence(builder.Configuration)
	.AddWebApi(builder.Configuration);

try
{
	var app = builder.Build();

	app.UseCors(PolicyConstants.CorsClients);

	app.UseHealthChecks("/health");
	app.UseRouting();

	app.UseMiddleware<ExceptionHandlingMiddleware>();
	app.UseSwagger();

	app.UseEndpoints(endpoints =>
	{
		endpoints.MapControllers();
		endpoints.MapHealthChecks("/health");
	});

	using (var scope = app.Services.CreateScope())
	{
		var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

		try
		{
			logger.LogInformation("Migrating application database");

			var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			ctx.Database.Migrate();

			logger.LogInformation("Application database migration done");
		}
		catch (Exception ex)
		{
			logger.LogError(ex,
				"An error occurred while migrating the application database: {Message}",
				ex.Message);
			throw;
		}
	}


	app.Run();
}
catch (Exception ex)
{
	Log.Logger.Fatal(ex, "Host terminated unexpectedly");
	throw;
}
finally
{
	Log.CloseAndFlush();
}