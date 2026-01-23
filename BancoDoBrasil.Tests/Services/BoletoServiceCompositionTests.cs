using BancoDoBrasil.Services;
using BancoDoBrasil.Tests.Builders;
using BancoDoBrasil.Tests.Fakes;
using FluentAssertions;

namespace BancoDoBrasil.Tests.Services;

public class BoletoServiceCompositionTests
{
    [Fact]
    public async Task RegistrarAsync_RequestCompleto_ExecutaPipelineCompleto()
    {
        // Given
        var fakeClient = new FakeBancoDoBrasilHttpClient();
        var service = new BoletoService(fakeClient);
        var request = new RegistrarBoletoRequestBuilder().Build();

        // When
        await service.RegistrarAsync(request, CancellationToken.None);

        // Assert — efeitos do pipeline
        request.numeroTituloCliente.Should().StartWith("000");
        request.pagador.uf.Should().Be("SP");
        request.indicadorPix.Should().Be("N");

        // Assert — serialização ocorreu
        fakeClient.UltimoJsonEnviado.Should().NotBeNullOrWhiteSpace();
        fakeClient.UltimoJsonEnviado.Should().Contain("\"numeroCarteira\"");

    }
}