using BancoDoBrasil.Formatting;
using FluentAssertions;
using Xunit;

namespace BancoDoBrasil.Tests.Formatting;
public class BbStringFormatterTests
{
    [Fact]
    public void NormalizeUpper_DeveNormalizarERespeitarTamanho()
    {
        var texto = "  teste de string  ";
        var resultado = BbStringFormatter.NormalizeUpper(texto, 10);
        resultado.Should().Be("TESTE DE S");
    }

    [Fact]
     public void NormalizeUpper_StringNula_DeveRetornarVazio()
    {
        var mensagem = "Linha 1\r\nLinha 2";
        var resultado = BbStringFormatter.NormalizarMensagemBloqueto(mensagem);
        resultado.Should().Be("Linha 1 Linha 2");
    }

    [Fact]
    public void NormalizarMensagemBloqueto_MensagemMuitoGrande_DeveLancarExcecao()
    {
        var mensagem = new string('A', 166);
        Action act = () => BbStringFormatter.NormalizarMensagemBloqueto(mensagem);
        act.Should().Throw<ArgumentException>().WithMessage("*165 caracteres*");
    }
}