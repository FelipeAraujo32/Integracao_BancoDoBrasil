using BancoDoBrasil.Exceptions;
using BancoDoBrasil.Validation.Payload;
using FluentAssertions;
using Xunit;

namespace BancoDoBrasil.Tests.Validation.Payload;

public class RegistrarBoletoPayloadValidatorTests
{
    [Fact]
    public void Validate_PayloadValido_NaoDeveFalhar()
    {
        var json = """
        {
            "numeroCarteira": 17,
            "valorOriginal": 100
        }
        """;

        Action act = () => RegistrarBoletoPayloadValidator.Validate(json);

        act.Should().NotThrow();
    }

    [Fact]
    public void Validate_CampoObrigatorioNulo_DeveFalhar()
    {
        var json = """
        {
            "numeroCarteira": null
        }
        """;

        Action act = () => RegistrarBoletoPayloadValidator.Validate(json);

        act.Should().Throw<BancoDoBrasilValidationException>();
    }
}
