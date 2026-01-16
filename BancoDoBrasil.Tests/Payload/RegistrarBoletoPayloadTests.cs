using System.Text.Json;
using BancoDoBrasil.Validation.Payload;
using BancoDoBrasil.Tests.Common;
using Xunit;

namespace BancoDoBrasil.Tests.Payload;

public class RegistrarBoletoPayloadTests
{
    [Fact]
    public void Payload_Valido_Nao_Deve_Lancar_Excecao()
    {
        var request = RequestFactory.CriarValido();
        var json = JsonSerializer.Serialize(
            request,
            JsonOptionsFactory.Create());

        RegistrarBoletoPayloadValidator.Validate(json);
    }
}
