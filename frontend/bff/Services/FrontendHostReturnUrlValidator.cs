using Duende.Bff;
using Microsoft.Extensions.Options;
using Resto.Bff.Configuration;

namespace Resto.Bff.Services;

// make sure the register this implementation AFTER calling .AddBff, otherwise the default implementation
// will be used

internal sealed class FrontendHostReturnUrlValidator : IReturnUrlValidator
{
    private readonly FrontendSettings _settings;

    public FrontendHostReturnUrlValidator(IOptions<FrontendSettings> configuration)
    {
        _settings = configuration.Value;
    }

    public Task<bool> IsValidAsync(string returnUrl)
    {
        var uri = new Uri(returnUrl);
        var result = _settings.ClientUri is not null &&
                     uri.Host == _settings.ClientUri.Host &&
                     uri.Port == _settings.ClientUri.Port;
        return Task.FromResult(result);
    }
}