using Duende.Bff;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.HttpOverrides;
using Resto.Bff.Configuration;
using Resto.Bff.Services;

namespace Resto.Bff;

internal static class DependencyInjection
{
    #region Configuration

    internal static IServiceCollection AddConfiguration(this IServiceCollection services)
    {
        services
            .AddValidatedSettings<RemoteApiSettings>(RemoteApiSettings.SectionName)
            .AddValidatedSettings<FrontendSettings>(FrontendSettings.SectionName);
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

    #region BFF Host

    internal static IServiceCollection AddRestoBff(this IServiceCollection services)
    {
        services
            .AddBff()
            .AddRemoteApis();
        services.AddTransient<IReturnUrlValidator, FrontendHostReturnUrlValidator>();
        
        services
            .Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            // Only loopback proxies are allowed by default. Clear that restriction because forwarders are
            // being enabled by explicit configuration.
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        services
            .AddHealthChecks();

        
        return services;
    }

    #endregion
}