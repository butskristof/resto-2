namespace Resto.Common.Configuration;

/// <summary>
/// Client information to use in e.g. CORS policies
/// </summary>
public sealed class ClientSettings
{
	public const string SectionName = "Clients";
	
	public string[] ClientUrls { get; init; } = [];
}