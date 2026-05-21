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

namespace BancoDoBrasil.Services;

public sealed class BoletoService
{
    private readonly IBancoDoBrasilHttpClient _client;

    public BoletoService(IBancoDoBrasilHttpClient client)
    {
        _client = client;
    }

    public async Task<RegistrarBoletoResponse> RegistrarAsync(
        RegistrarBoletoRequest request,
        CancellationToken ct)
    {
        PrepararRequest(request);

        var jsonPayload = SerializarPayload(request);
        RegistrarBoletoPayloadValidator.Validate(jsonPayload);

        var responseJson = await EnviarParaBbAsync(jsonPayload, ct);
        return DesserializarResposta(responseJson);
    }

    private static void PrepararRequest(RegistrarBoletoRequest request)
    {
        RegistrarBoletoStructureValidator.Validate(request);
        MontarNumeroTituloCliente(request);
        ExecutarValidacoesDeNegocio(request);
        AplicarFormatacoesStringBb(request);
    }

    private static string SerializarPayload(RegistrarBoletoRequest request)
    {
        return JsonSerializer.Serialize(request, JsonOptions);
    }

    private async Task<string> EnviarParaBbAsync(
        string jsonPayload,
        CancellationToken ct)
    {
        var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            "boletos")
        {
            Content = new StringContent(
                jsonPayload,
                System.Text.Encoding.UTF8,
                "application/json")
        };

        var response = await _client.SendAsync(httpRequest, ct);
        var responseBody = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            throw new BancoDoBrasilIntegrationException(
                $"Erro BB {(int)response.StatusCode}: {responseBody}");
        }

        return responseBody;
    }

    private static RegistrarBoletoResponse DesserializarResposta(string json)
    {
        return JsonSerializer.Deserialize<RegistrarBoletoResponse>(
            json,
            JsonOptions)!;
    }

    private static void ExecutarValidacoesDeNegocio(RegistrarBoletoRequest request)
    {
        RegistrarBoletoBusinessValidator.Validate(request);
        DescontoBusinesValidator.ValidarDesconto(request);
        MultaBussinessValidator.ValidarMulta(request);
        PagadorBusinessValidator.ValidarPagador(request);
        BeneficiarioBusinessValidator.ValidarBeneficiario(request.beneficiarioFinal);
    }
    
    private static void AplicarFormatacoesStringBb(RegistrarBoletoRequest r)
    {
        r.campoUtilizacaoBeneficiario = BbStringFormatter.NormalizeUpper(r.campoUtilizacaoBeneficiario, 25);
        r.mensagemBloquetoOcorrencia = BbStringFormatter.NormalizarMensagemBloqueto(r.mensagemBloquetoOcorrencia);
        r.pagador.uf = BbStringFormatter.NormalizeUpper(r.pagador.uf, 2);
        r.indicadorPix = BbStringFormatter.NormalizeUpper(r.indicadorPix, 1);
    }

    private static void MontarNumeroTituloCliente(RegistrarBoletoRequest r)
    {
        var convenio = r.numeroConvenio.ToString("D7");
        var controle = r.numeroControle.ToString("D10");

        r.numeroTituloCliente = $"000{convenio}{controle}";
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        Converters =
        {
            new BbDateJsonConverter()
        }
    };
}
