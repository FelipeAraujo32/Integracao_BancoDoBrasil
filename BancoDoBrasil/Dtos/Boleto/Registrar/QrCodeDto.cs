namespace BancoDoBrasil.Dtos.Boleto.Registrar;

public sealed class QrCodeDto
{
    public string url { get; set; } = default!;
    public string txId { get; set; } = default!;
    public string emv { get; set; } = default!;
}
