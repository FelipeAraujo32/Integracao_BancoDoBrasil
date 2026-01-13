namespace BancoDoBrasil.Configuration;

public sealed class BancoDoBrasilOptions
{
    public string ApiBaseUrl { get; init; } = default!;
    public string OAuthBaseUrl { get; init; } = default!;
    public string ClientId { get; init; } = default!;
    public string ClientSecret { get; init; } = default!;
    public string DeveloperApplicationKey { get; init; } = default!;
}
