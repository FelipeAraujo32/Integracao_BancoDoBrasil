using System.Text.Json;
using BancoDoBrasil.Exceptions;

namespace BancoDoBrasil.Validation.Payload;

internal static class RegistrarBoletoPayloadValidator
{
    public static void Validate(object request, JsonSerializerOptions options)
    {
        var json = JsonSerializer.Serialize(request, options);

        if (json.Contains(":null"))
            throw new BancoDoBrasilValidationException(
                "Payload contém campos nulos");

        if (!json.Contains("numeroCarteira"))
            throw new BancoDoBrasilValidationException(
                "Payload inválido: numeroCarteira ausente");
    }
}
