namespace Resto.Common.Configuration;

/// <summary>
/// Client information to use in e.g. CORS policies
/// </summary>
public interface IClientConfiguration
{
	string[] ClientUrls { get; }
}

public class ClientConfiguration : IClientConfiguration
{
	public string[] ClientUrls { get; init; } = Array.Empty<string>();
}