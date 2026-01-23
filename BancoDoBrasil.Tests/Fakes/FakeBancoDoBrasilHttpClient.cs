using System.Net;
using System.Net.Http;
using System.Text;
using BancoDoBrasil.Http;

namespace BancoDoBrasil.Tests.Fakes;

public sealed class FakeBancoDoBrasilHttpClient : IBancoDoBrasilHttpClient
{
    public string? UltimoJsonEnviado { get; private set; }

    public async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (request.Content != null)
        {
            UltimoJsonEnviado =
                await request.Content.ReadAsStringAsync(cancellationToken);
        }

        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(
                "{}",
                Encoding.UTF8,
                "application/json")
        };
    }
}
