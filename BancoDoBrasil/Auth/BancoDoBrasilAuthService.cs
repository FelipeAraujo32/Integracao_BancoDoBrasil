using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BancoDoBrasil.Auth;

public sealed class BancoDoBrasilAuthService
{
    private readonly HttpClient _http;
    private string? _token;
    private DateTime _expiresAt;

    public BancoDoBrasilAuthService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> GetTokenAsync(
        string clientId,
        string clientSecret,
        CancellationToken ct)
    {
        if (_token != null && DateTime.UtcNow < _expiresAt)
            return _token;

        var auth = Convert.ToBase64String(
            Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

        var request = new HttpRequestMessage(
            HttpMethod.Post,
            "oauth/token");

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Basic", auth);

        request.Content = new FormUrlEncodedContent(
            new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials"
            });

        var response = await _http.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(ct);
        var doc = JsonDocument.Parse(json);

        _token = doc.RootElement
            .GetProperty("access_token")
            .GetString();

        var expiresIn = doc.RootElement
            .GetProperty("expires_in")
            .GetInt32();

        _expiresAt = DateTime.UtcNow.AddSeconds(expiresIn - 60);

        return _token!;
    }
}
