using Microsoft.Extensions.Options;
using Resto.Bff;
using Resto.Bff.Configuration;

var builder = WebApplication.CreateBuilder(args);
var isProduction = builder.Environment.IsProduction();

builder.Services
    .AddConfiguration()
    .AddRestoBff();

var app = builder.Build();

app.UseForwardedHeaders();
if (isProduction) // serve SPA assets from wwwroot
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
}

app.UseRouting();

app.UseBff();

app.MapHealthChecks("/health");
app.MapBffManagementEndpoints();
var remoteApiSettings = app.Services
    .GetRequiredService<IOptions<RemoteApiSettings>>()
    .Value;
app.MapRemoteBffApiEndpoint("/api", remoteApiSettings.RestoApiUrl);

// serve index for everything that's not either a static asset or a configured route (bff, api proxy, ...)
if (isProduction)
    app.MapFallbackToFile("index.html");

app.Run();