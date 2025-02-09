namespace Resto.Bff.Configuration;

internal sealed class RemoteApiSettings
{
    internal const string SectionName = "RemoteApis";
    
    public required string RestoApiUrl { get; init; }
}