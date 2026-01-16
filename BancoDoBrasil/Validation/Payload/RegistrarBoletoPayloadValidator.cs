using System.Text.Json;
using BancoDoBrasil.Exceptions;

namespace BancoDoBrasil.Validation.Payload;

public static class RegistrarBoletoPayloadValidator
{
    public static void Validate(string json)
    {
        using var doc = JsonDocument.Parse(json);
        var campoNulo = EncontrarCampoNulo(doc.RootElement);

        if (campoNulo != null)
            throw new BancoDoBrasilValidationException(
                $"Campo obrigatório nulo no payload: {campoNulo}");

        if (!doc.RootElement.TryGetProperty("numeroCarteira", out _))
            throw new BancoDoBrasilValidationException(
                "Payload inválido: numeroCarteira ausente.");
    }

    private static string? EncontrarCampoNulo(
        JsonElement element,
        string caminho = "")
    {
        if (element.ValueKind == JsonValueKind.Null)
        {
            return CamposQuePodemSerNull.Contains(caminho)
                ? null
                : caminho;
        }

        if (element.ValueKind == JsonValueKind.Object)
        {
            foreach (var prop in element.EnumerateObject())
            {
                var novoCaminho = string.IsNullOrEmpty(caminho)
                    ? prop.Name
                    : $"{caminho}.{prop.Name}";

                var resultado = EncontrarCampoNulo(prop.Value, novoCaminho);
                if (resultado != null)
                    return resultado;
            }
        }

        if (element.ValueKind == JsonValueKind.Array)
        {
            var index = 0;
            foreach (var item in element.EnumerateArray())
            {
                var arrayCaminho = $"{caminho}[{index}]";

                var resultado = EncontrarCampoNulo(item, arrayCaminho);
                if (resultado != null)
                    return resultado;

                index++;
            }
        }

        return null;
    }

    private static readonly HashSet<string> CamposQuePodemSerNull = new()
    {
        "valorAbatimento",
        "quantidadeDiasProtesto",
        "indicadorAceiteTituloVencido",
        "numeroDiasLimiteRecebimento",
        "descricaoTipoTitulo",
        "mensagemBloquetoOcorrencia",
        "desconto",
        "segundoDesconto",
        "terceiroDesconto",
        "jurosMora",
        "multa",
        "beneficiarioFinal",
        "indicadorPix"
    };
}
