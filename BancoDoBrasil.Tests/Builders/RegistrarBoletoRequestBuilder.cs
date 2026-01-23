using BancoDoBrasil.Dtos.Boleto.Registrar;

namespace BancoDoBrasil.Tests.Builders;

public class RegistrarBoletoRequestBuilder
{
    private readonly RegistrarBoletoRequest _request = new()
    {
        numeroConvenio = 1234567,
        numeroCarteira = 17,
        numeroVariacaoCarteira = 19,
        codigoModalidade = 1,

        dataEmissao = DateTime.Today,
        dataVencimento = DateTime.Today.AddDays(5),

        valorOriginal = 100m,
        valorAbatimento = null,

        quantidadeDiasProtesto = null, // usa regra do convênio
        quantidadeDiasNegativacao = null,
        orgaoNegativador = null,

        indicadorAceiteTituloVencido = "N",
        numeroDiasLimiteRecebimento = 0,

        codigoAceite = "A",
        codigoTipoTitulo = 2,
        descricaoTipoTitulo = "DM",

        indicadorPermissaoRecebimentoParcial = "N",

        numeroTituloBeneficiario = "TITULO123",
        campoUtilizacaoBeneficiario = null,
        numeroTituloCliente = null,

        mensagemBloquetoOcorrencia = null,

        indicadorPix = "N",

        pagador = new PagadorDto
        {
            tipoInscricao = 1,
            numeroInscricao = 12345678901,
            nome = "PAGADOR TESTE",
            endereco = "RUA A",
            bairro = "CENTRO",
            cidade = "SAO PAULO",
            uf = "SP",
            cep = 12345678,
            telefone = "11999999999"
        },

        beneficiarioFinal = null,
        numeroControle = 123456789,
    
    };

    public RegistrarBoletoRequest Build() => _request;

    public RegistrarBoletoRequestBuilder ComValor(decimal valor)
    {
        _request.valorOriginal = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComPagadorNulo()
    {
        _request.pagador = null!;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComNumeroConvenio(int valor)
    {
        _request.numeroConvenio = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComCodigoModalidade(int valor)
    {
        _request.codigoModalidade = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComDataEmissao(DateTime date)
    {
        _request.dataEmissao = date;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComDataVencimento(DateTime date)
    {
        _request.dataVencimento = date;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComValorAbatimento(decimal valor)
    {
        _request.valorAbatimento = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComQuantidadeDiasProtesto(int valor)
    {
        _request.quantidadeDiasProtesto = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComIndicadorAceite(string valor)
    {
        _request.codigoAceite = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComNumeroDiasLimiteRecebimento(int valor)
    {
        _request.numeroDiasLimiteRecebimento = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComNumeroCarteira(int valor)
    {
        _request.numeroCarteira = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComCodigoTipoTitulo(int valor)
    {
        _request.codigoTipoTitulo = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComIndicadorPermissaoRecebimentoParcial(string valor)
    {
        _request.indicadorPermissaoRecebimentoParcial = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComNumeroTituloBeneficiario(string valor)
    {
        _request.numeroTituloBeneficiario = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComNumeroControle(long valor)
    {
        _request.numeroControle = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComTipoInscricaoPagador(int valor)
    {
        _request.pagador.tipoInscricao = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComNumeroInscricaoPagador(long valor)
    {
        _request.pagador.numeroInscricao = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComNomePagador(string valor, int qnt)
    {
        _request.pagador.nome = string.Concat(Enumerable.Repeat(valor, qnt));
        return this;
    }

    public RegistrarBoletoRequestBuilder ComCepPagador(int valor)
    {
        _request.pagador.cep = valor;
        return this;
    }

    public RegistrarBoletoRequestBuilder ComUfPagador(string valor)
    {
        _request.pagador.uf = valor;
        return this;
    }
}
