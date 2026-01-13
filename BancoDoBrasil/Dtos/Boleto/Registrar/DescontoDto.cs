namespace BancoDoBrasil.Dtos.Boleto.Registrar;

public sealed class DescontoDto
{
    public int tipo { get; set; }
    public DateTime? dataExpiracao { get; set; }
    public decimal? porcentagem { get; set; }
    public decimal? valor { get; set; }
}

