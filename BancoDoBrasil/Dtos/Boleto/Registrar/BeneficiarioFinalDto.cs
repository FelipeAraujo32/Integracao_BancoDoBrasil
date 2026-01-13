namespace BancoDoBrasil.Dtos.Boleto.Registrar;

public sealed class BeneficiarioFinalDto
{
    public int? tipoInscricao { get; set; }
    public long? numeroInscricao { get; set; }
    public string? nome { get; set; } = default!;
}
