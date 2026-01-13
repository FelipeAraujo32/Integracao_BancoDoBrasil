namespace BancoDoBrasil.Dtos.Boleto.Registrar;

public sealed class MultaDto
{
    public int tipo { get; set; }
    public DateTime? data { get; set; }
    public decimal? porcentagem { get; set; }
    public decimal? valor { get; set; }
}