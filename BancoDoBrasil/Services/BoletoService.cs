using System.Net.Http.Json;
using System.Text.Json;
using BancoDoBrasil.Dtos.Boleto.Registrar;
using BancoDoBrasil.Http;
using BancoDoBrasil.Validation.Structure;
using BancoDoBrasil.Validation.Business;
using BancoDoBrasil.Validation.Payload;
using BancoDoBrasil.Formatting;
using BancoDoBrasil.Exceptions;
using BancoDoBrasil.Serialization;
using System.Text.Json.Serialization;

namespace BancoDoBrasil.Services;

public sealed class BoletoService
{
    private readonly BancoDoBrasilHttpClient _client;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    public BoletoService(BancoDoBrasilHttpClient client)
    {
        _client = client;
    }

    public async Task<RegistrarBoletoResponse> RegistrarAsync(
        RegistrarBoletoRequest request,
        CancellationToken ct)
    {
        // Validação estrutural 
        RegistrarBoletoStructureValidator.Validate(request);

        // Validação de regras do BB
        RegistrarBoletoBusinessValidator.Validate(request);
        DescontoBusinesValidator.ValidarDesconto(request);
        MultaBussinessValidator.ValidarMulta(request);
        PagadorBusinessValidator.ValidarPagador(request);
        BeneficiarioBusinessValidator.ValidarBeneficiario(request.beneficiarioFinal);

        MontarNumeroTituloCliente(request);

        // Formatação padrão BB (ANTES de serializar)
        AplicarFormatacoesBb(request);

        // Validação do payload final (JSON)
        RegistrarBoletoPayloadValidator.Validate(request, JsonOptions);

        // Montagem do request HTTP
        var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            "boletos")
        {
            Content = JsonContent.Create(request, options: JsonOptions)
        };

        // Envio para o BB
        var response = await _client.SendAsync(httpRequest, ct);
        var responseBody = await response.Content.ReadAsStringAsync(ct);

        // Tratamento de erro BB
        if (!response.IsSuccessStatusCode)
        {
            throw new BancoDoBrasilIntegrationException(
                $"Erro BB {(int)response.StatusCode}: {responseBody}");
        }

        // Desserialização segura
        return JsonSerializer.Deserialize<RegistrarBoletoResponse>(
            responseBody,
            JsonOptions)!;
    }

    /// <summary>
    /// Centraliza TODA formatação exigida pelo BB
    /// </summary>
    private static void AplicarFormatacoesBb(RegistrarBoletoRequest r)
    {
        // Datas - converter DateTime para string no formato BB
        string dataEmissao = BbDateFormatter.Format(r.dataEmissao); // Realizar o tratamento de datas.
        string dataVencimento = BbDateFormatter.Format(r.dataVencimento);// Realizar o tratamento de datas.

        // Strings principais
        if (!string.IsNullOrWhiteSpace(r.campoUtilizacaoBeneficiario))
        {
            r.campoUtilizacaoBeneficiario =
                BbStringFormatter.Normalize(r.campoUtilizacaoBeneficiario, 25);
        }

        // Pagador
        r.pagador.nome =
            BbStringFormatter.Normalize(r.pagador.nome, 60);

        r.pagador.endereco =
            BbStringFormatter.Normalize(r.pagador.endereco, 60);

        r.pagador.bairro =
            BbStringFormatter.Normalize(r.pagador.bairro, 30);

        r.pagador.cidade =
            BbStringFormatter.Normalize(r.pagador.cidade, 30);

        r.pagador.uf =
            BbStringFormatter.Normalize(r.pagador.uf, 2);

        r.mensagemBloquetoOcorrencia = BbStringFormatter.NormalizarMensagemBloqueto(r.mensagemBloquetoOcorrencia);    
    }


    private static void MontarNumeroTituloCliente(RegistrarBoletoRequest r)
    {
        var convenio = r.numeroConvenio.ToString("D7");
        var controle = r.numeroControle.ToString("D10");

        r.numeroTituloCliente = $"000{convenio}{controle}";
    }

}
