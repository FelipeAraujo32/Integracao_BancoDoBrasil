namespace BancoDoBrasil.Http;

public interface IBancoDoBrasilHttpClient
{
    Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken);
}
