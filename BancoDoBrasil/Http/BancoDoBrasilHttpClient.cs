using System.Net.Http.Headers;
using BancoDoBrasil.Auth;
using BancoDoBrasil.Configuration;

namespace BancoDoBrasil.Http;

public sealed class BancoDoBrasilHttpClient
{
    private readonly HttpClient _http;
    private readonly BancoDoBrasilAuthService _auth;
    private readonly BancoDoBrasilOptions _options;

    public BancoDoBrasilHttpClient(
        HttpClient http,
        BancoDoBrasilAuthService auth,
        BancoDoBrasilOptions options)
    {
        _http = http;
        _auth = auth;
        _options = options;
    }

    public async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken ct)
    {
        var token = await _auth.GetTokenAsync(
            _options.ClientId,
            _options.ClientSecret,
            ct);

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        request.Headers.Add(
            "x-developer-application-key",
            _options.DeveloperApplicationKey);
        Console.WriteLine($"➡️ BB REQUEST: {request.Method} {request.RequestUri}");
        return await _http.SendAsync(request, ct);
    }
}
