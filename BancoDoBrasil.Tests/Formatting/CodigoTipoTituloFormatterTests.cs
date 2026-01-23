using BancoDoBrasil.Exceptions;
using BancoDoBrasil.Formatting;
using FluentAssertions;
using Xunit;

namespace BancoDoBrasil.Tests.Formatting;

public class CodigoTipoTituloFormatterTests
{
    [Theory]
    [InlineData(1, "CH")]
    [InlineData(2, "DM")]
    [InlineData(12, "NP")]
    [InlineData(33, "BA")]
    public void ObterDescricao_CodigosValidos_DeveRetornarDescricao(int codigo, string esperado)
    {
        var resultado = CodigoTipoTituloFormatter.ObterDescricao(codigo);
        resultado.Should().Be(esperado);
    }

    [Fact]
    public void ObterDescricao_CodigoInvalido_DeveLancarExcecao()
    {
        Action act = () => CodigoTipoTituloFormatter.ObterDescricao(99);
        act.Should().Throw<BancoDoBrasilValidationException>();
    }
}