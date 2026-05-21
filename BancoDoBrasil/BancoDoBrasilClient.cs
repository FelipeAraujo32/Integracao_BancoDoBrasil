using BancoDoBrasil.Auth;
using BancoDoBrasil.Configuration;
using BancoDoBrasil.Http;
using BancoDoBrasil.Services;

namespace BancoDoBrasil;

public sealed class BancoDoBrasilClient
{
    public BoletoService Boleto { get; }

    public BancoDoBrasilClient(
        BancoDoBrasilOptions options,
        HttpClient httpApi,
        HttpClient httpOAuth)
    {
        var authService = new BancoDoBrasilAuthService(httpOAuth);

        var bbHttpClient = new BancoDoBrasilHttpClient(
            httpApi,
            authService,
            options);

        Boleto = new BoletoService(bbHttpClient);
    }
}
