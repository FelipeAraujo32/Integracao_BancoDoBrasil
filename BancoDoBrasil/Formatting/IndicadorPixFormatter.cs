using BancoDoBrasil.Dtos.Boleto.Registrar;

namespace BancoDoBrasil.Formatting;

public static class IndicadorPixFormatter
{
    public static void Normalize(RegistrarBoletoRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            request.indicadorPix =
                string.Equals(request.indicadorPix, "S", StringComparison.OrdinalIgnoreCase)
                    ? "S"
                    : "N";
        }
}