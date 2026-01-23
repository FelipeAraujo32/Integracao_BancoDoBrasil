using BancoDoBrasil.Dtos.Boleto.Registrar;
using BancoDoBrasil.Exceptions;
using BancoDoBrasil.Tests.Builders;
using FluentAssertions;

namespace BancoDoBrasil.Tests.Validation.Business;

public class PagadorBusinessValidatorTests
{
    [Fact]
    public void ValidarPagador_PagadorValido_NaoDeveLancarExcecao()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .Build();

        Action act = () => PagadorBusinessValidator.ValidarPagador(request);

        act.Should().NotThrow();
    }

    [Fact]
    public void ValidarPagador_PagadorNulo_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComPagadorNulo()
            .Build();

        Action act = () => PagadorBusinessValidator.ValidarPagador(request);

        act.Should()
            .Throw<BancoDoBrasilValidationException>()
            .WithMessage("*Pagador é obrigatório*");
    }

    [Fact]
    public void ValidarPagador_CpfComTipoInscricaoInvalido_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComTipoInscricaoPagador(2)
            .Build();

        Action act = () => PagadorBusinessValidator.ValidarPagador(request);

        act.Should()
            .Throw<BancoDoBrasilValidationException>()
            .WithMessage("*CPF*");
    }

    [Fact]
    public void ValidarPagador_DocumentoComTamanhoInvalido_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComNumeroInscricaoPagador(123)
            .Build();

        Action act = () => PagadorBusinessValidator.ValidarPagador(request);

        act.Should()
            .Throw<BancoDoBrasilValidationException>()
            .WithMessage("*numeroInscricao*");
    }

    [Fact]
    public void ValidarPagador_NomeMaiorQue60Caracteres_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComNomePagador("ABC", 61)
            .Build();

        Action act = () => PagadorBusinessValidator.ValidarPagador(request);

        act.Should()
            .Throw<BancoDoBrasilValidationException>()
            .WithMessage("*60 caracteres*");
    }

    [Fact]
    public void ValidarPagador_CepMenorOuIgualZero_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComCepPagador(0)
            .Build();

        Action act = () => PagadorBusinessValidator.ValidarPagador(request);

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*CEP inválido*");
    }

    [Fact]
    public void ValidarPagador_UfComTamanhoInvalido_DeveFalhar()
    {
        var request = new RegistrarBoletoRequestBuilder()
            .ComUfPagador("S")
            .Build();

        Action act = () => PagadorBusinessValidator.ValidarPagador(request);

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*UF deve ter exatamente 2 caracteres*");
    }

    [Fact]
    public void ValidarPagador_UfInexistente_DeveFalhar()
    {

        var request = new RegistrarBoletoRequestBuilder()
            .ComUfPagador("XX")
            .Build();

        Action act = () => PagadorBusinessValidator.ValidarPagador(request);

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("*UF inválida*");
    }

}