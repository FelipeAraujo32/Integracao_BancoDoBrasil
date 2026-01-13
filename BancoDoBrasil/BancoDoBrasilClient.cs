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
        // Auth
        var authService = new BancoDoBrasilAuthService(httpOAuth);

        // Http Client central do BB
        var bbHttpClient = new BancoDoBrasilHttpClient(
            httpApi,
            authService,
            options);

        // Services expostos
        Boleto = new BoletoService(bbHttpClient);
    }
}
