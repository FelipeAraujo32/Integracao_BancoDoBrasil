using BancoDoBrasil.Exceptions;
using BancoDoBrasil.Validation.Business;
using BancoDoBrasil.Tests.Builders;
using FluentAssertions;
using Xunit;

namespace BancoDoBrasil.Tests.Validation.Business;

public class RegistrarBoletoBusinessValidatorTests
{
    [Fact]
    public void Validate_RequestValido_NaoDeveLancarExcecao()
    {
        var request = new RegistrarBoletoRequestBuilder().Build();

        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().NotThrow();
    }

    [Fact]
    public void Validate_ValorOriginalZero_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComValor(0)
            .Build();

        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*valorOriginal*");
    }

    //Número do Convênio

    [Fact]
    public void Validate_NumeroConvenioNegativo_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder().ComNumeroConvenio(-1).Build();

        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*convênio*");
    }

    [Fact]
    public void Validate_NumeroConvenioMaiorQue7Digitos_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComNumeroConvenio(10_000_000)
            .Build();

        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*7 dígitos*");
    }

    //Código Modalidade

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(99)]
    public void Validate_CodigoModalidadeInvalido_DeveFalhar(int modalidade)
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComCodigoModalidade(modalidade)
            .Build();
        
        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*codigoModalidade*");
    }

    //Datas

    [Fact]
    public void Validate_DataEmissaoNoPassado_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComDataEmissao(DateTime.Today.AddDays(-1))
            .Build();

        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*dataEmissao*");
    }

    [Fact]
    public void Validate_DataVencimentoMenorQueEmissao_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComDataEmissao(DateTime.Today.AddDays(1))
            .ComDataVencimento(DateTime.Today)
            .Build();

        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*dataVencimento*");
    }

    //Valores

    [Fact]
    public void Validate_ValorAbatimentoMenorOuIgualZero_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComValorAbatimento(0)
            .Build();

        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*valorAbatimento*");
    }

    // Protesto

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(30)]
    [InlineData(34)]
    public void Validate_QuantidadeDiasProtestoInvalida_DeveFalhar(int dias)
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComQuantidadeDiasProtesto(dias)
            .Build();

        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*quantidadeDiasProtesto*");
    }

    // Aceite título vencido

    [Fact]
    public void Validate_IndicadorAceiteInvalido_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComIndicadorAceite("X")
            .Build();

        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*Aceite*");
    }

    [Fact]
    public void Validate_NumeroDiasLimiteRecebimentoMaiorQueZeroSemAceite_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComIndicadorAceite("N")
            .ComNumeroDiasLimiteRecebimento(5)
            .Build();

        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*numeroDiasLimiteRecebimento*");
    }

    // Código Tipo Título

    [Fact]
    public void Validate_CodigoTipoTituloInvalidoParaCarteira_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComNumeroCarteira(17)
            .ComCodigoTipoTitulo(99)
            .Build();

        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*codigoTipoTitulo*");
    }


    // Permissão de recebimento parcial

    [Fact]
    public void Validate_IndicadorPermissaoRecebimentoParcialInvalido_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComIndicadorPermissaoRecebimentoParcial("X")
            .Build();

        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*RecebimentoParcial*");
    }

    // Número título beneficiário

    [Fact]
    public void Validate_NumeroTituloBeneficiarioComMaisDe15Caracteres_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComNumeroTituloBeneficiario("1234567890123456")
            .Build();

        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*15 caracteres*");
    }

    // Número Controle

    [Fact]
    public void Validate_NumeroControleMaiorQue10Digitos_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComNumeroControle(10_000_000_000)
            .Build();

        Action act = () => RegistrarBoletoBusinessValidator.Validate(request);

        act.Should().Throw<BancoDoBrasilValidationException>()
            .WithMessage("*NumeroControle*");
    }
}