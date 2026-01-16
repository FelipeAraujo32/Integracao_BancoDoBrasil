using BancoDoBrasil.Dtos.Boleto.Registrar;

namespace BancoDoBrasil.Tests.Common;

public static class RequestFactory
{
    public static RegistrarBoletoRequest CriarValido()
    {
        return new RegistrarBoletoRequest
        {
            numeroConvenio = 3128557,
            numeroCarteira = 17,
            numeroVariacaoCarteira = 35,
            codigoModalidade = 1,

            dataEmissao = DateTime.Today,
            dataVencimento = DateTime.Today.AddDays(5),

            valorOriginal = 150.10m,
            codigoAceite = "N",
            indicadorPermissaoRecebimentoParcial = "N",
            numeroControle = 1234567890,

            pagador = new PagadorDto
            {
                tipoInscricao = 1,
                numeroInscricao = 97965940132,
                nome = "JOAO DA SILVA",
                endereco = "RUA TESTE",
                cidade = "BRASILIA",
                bairro = "CENTRO",
                uf = "DF",
                cep = 77458000
            }
        };
    }
}
