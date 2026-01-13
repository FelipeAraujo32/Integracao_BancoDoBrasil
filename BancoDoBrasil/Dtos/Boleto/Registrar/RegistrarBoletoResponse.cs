namespace BancoDoBrasil.Dtos.Boleto.Registrar;

public sealed class RegistrarBoletoResponse
{
    public string numero { get; set; } = default!;
    public int numeroCarteira { get; set; }
    public int numeroVariacaoCarteira { get; set; }
    public int codigoCliente { get; set; }

    public string linhaDigitavel { get; set; } = default!;
    public string codigoBarraNumerico { get; set; } = default!;
    public int numeroContratoCobranca { get; set; }

    public BeneficiarioResponseDto beneficiario { get; set; } = default!;
    public QrCodeDto? qrCode { get; set; }

    public string urlImagemBoleto { get; set; } = default!;
    public string? observacao { get; set; }
}
