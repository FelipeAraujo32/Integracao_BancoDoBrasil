using System.Text.Json.Nodes;
using BancoDoBrasil.Services;
using BancoDoBrasil.Tests.Builders;
using BancoDoBrasil.Tests.Fakes;
using FluentAssertions;
using Xunit;

namespace BancoDoBrasil.Tests.Services;

public class BoletoServiceContractTests
{
    [Fact]
    public async Task RegistrarAsync_JSONFinal_DeveSeguirContratoBB()
    {
        // Arrange
        var fakeClient = new FakeBancoDoBrasilHttpClient();
        var service = new BoletoService(fakeClient);

        var request = new RegistrarBoletoRequestBuilder().Build();

        // Act
        await service.RegistrarAsync(request, CancellationToken.None);

        var jsonGerado = fakeClient.UltimoJsonEnviado!;
        var jsonEsperado = File.ReadAllText("Snapshots/registrar-boleto.json");

        // Assert
        var jsonGeradoNode = JsonNode.Parse(jsonGerado);
        var jsonEsperadoNode = JsonNode.Parse(jsonEsperado);

        JsonNode.DeepEquals(jsonGeradoNode, jsonEsperadoNode)
            .Should()
            .BeTrue();
    }
}