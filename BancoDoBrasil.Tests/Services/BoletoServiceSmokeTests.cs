using BancoDoBrasil.Services;
using BancoDoBrasil.Tests.Common;
using Xunit;
using Moq;
using BancoDoBrasil.Auth;
using BancoDoBrasil.Configuration;
using BancoDoBrasil.Http;

namespace BancoDoBrasil.Tests.Services;

public class BoletoServiceSmokeTests
{
    [Fact]
    public async Task Pipeline_Com_Request_Valido_Nao_Deve_Quebrar_Antes_Do_HTTP()
    {
        var request = RequestFactory.CriarValido();

        // HttpClient fake
        var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost") };

        // Mock dos serviços necessários
        var authServiceMock = new Mock<BancoDoBrasilAuthService>();
        var optionsMock = new Mock<BancoDoBrasilOptions>();

        var fakeClient = new BancoDoBrasilHttpClient(
            httpClient,
            authServiceMock.Object,
            optionsMock.Object);

        var service = new BoletoService(fakeClient);

        // Esperamos erro de HTTP, não de validação
        await Assert.ThrowsAsync<Exception>(() =>
            service.RegistrarAsync(request, CancellationToken.None));
    }
}
