namespace Resto.Common.Configuration;

/// <summary>
/// Client information to use in e.g. CORS policies
/// </summary>
public interface IClientOptions
{
	string[] ClientUrls { get; }
}

public class ClientOptions : IClientOptions
{
	public string[] ClientUrls { get; init; } = Array.Empty<string>();
}