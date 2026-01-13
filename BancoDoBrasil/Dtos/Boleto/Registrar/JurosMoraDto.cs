namespace BancoDoBrasil.Dtos.Boleto.Registrar;

public sealed class JurosMoraDto
{
    public int tipo { get; set; }
    public decimal? porcentagem { get; set; }
    public decimal? valor { get; set; }
}