using BancoDoBrasil;
using BancoDoBrasil.Configuration;
using BancoDoBrasil.Dtos.Boleto.Registrar;

Console.WriteLine("=== TESTE REGISTRAR BOLETO BB ===");

var options = new BancoDoBrasilOptions
{
    OAuthBaseUrl = "https://oauth.sandbox.bb.com.br",
    ClientId = "eyJpZCI6IjAxYzM0ZDctYzg5Yi00M2E5LWI4NmUiLCJjb2RpZ29QdWJsaWNhZG9yIjowLCJjb2RpZ29Tb2Z0d2FyZSI6MTU5ODE5LCJzZXF1ZW5jaWFsSW5zdGFsYWNhbyI6MX0",
    ClientSecret = "eyJpZCI6IjllM2M3NWEtY2U5MS0iLCJjb2RpZ29QdWJsaWNhZG9yIjowLCJjb2RpZ29Tb2Z0d2FyZSI6MTU5ODE5LCJzZXF1ZW5jaWFsSW5zdGFsYWNhbyI6MSwic2VxdWVuY2lhbENyZWRlbmNpYWwiOjEsImFtYmllbnRlIjoiaG9tb2xvZ2FjYW8iLCJpYXQiOjE3NjIxOTQ0NTY4MjB9",
    DeveloperApplicationKey = "3b560a4ead33425bbd22280013c2837e",
};

var httpApi = new HttpClient
{
    BaseAddress = new Uri("https://api.hm.bb.com.br/cobrancas/v2/")
};

var httpOAuth = new HttpClient
{
    BaseAddress = new Uri(options.OAuthBaseUrl)
};

var bb = new BancoDoBrasilClient(
    options,
    httpApi,
    httpOAuth);

var request = new RegistrarBoletoRequest
{
    numeroConvenio = 3128557,
    numeroCarteira = 17,
    numeroVariacaoCarteira = 35,
    codigoModalidade = 1,
    dataEmissao = 08.01.2026,
    dataVencimento = 15.01.2026,
    valorOriginal = 150.10m,
    valorAbatimento = 12.34m,
    quantidadeDiasProtesto = 0,
    indicadorAceiteTituloVencido = "N",
    numeroDiasLimiteRecebimento = 0,
    codigoAceite = "N",
    codigoTipoTitulo = 2,
    descricaoTipoTitulo = "DM",
    indicadorPermissaoRecebimentoParcial = "N",
    numeroTituloBeneficiario = "TESTE123",
    numeroTituloCliente = "00031285571234568891",
    indicadorPix = "S",
    pagador = new PagadorDto
    {
        tipoInscricao = 1,
        numeroInscricao = 97965940132,
        nome = "JOAO DA SILVA",
        endereco = "RUA TESTE",
        cep = 77458000,
        cidade = "BRASILIA",
        bairro = "CENTRO",
        uf = "DF",
        email = "teste@email.com",
    },
    beneficiarioFinal = new BeneficiarioFinalDto
    {
    tipoInscricao = 1,
    numeroInscricao = 66779051870,
    nome = "Dirceu Borboleta",
    }
};

try
{
    var response = await bb.Boleto.RegistrarAsync(
        request,
        CancellationToken.None);

    Console.WriteLine("✅ BOLETO REGISTRADO COM SUCESSO");
    Console.WriteLine($"Linha digitável: {response.linhaDigitavel}");
    Console.WriteLine($"URL boleto: {response.urlImagemBoleto}");
}
catch (Exception ex)
{
    Console.WriteLine("❌ ERRO AO REGISTRAR BOLETO");
    Console.WriteLine(ex.Message);
}

Console.WriteLine("=== FIM DO TESTE ===");
