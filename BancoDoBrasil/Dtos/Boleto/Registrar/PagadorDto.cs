namespace BancoDoBrasil.Dtos.Boleto.Registrar;

public sealed class PagadorDto
{
    public int tipoInscricao { get; set; }
    public long numeroInscricao { get; set; }

    public string? nome { get; set; } = default!;
    public string? endereco { get; set; } = default!;
    public int? cep { get; set; }

    public string? cidade { get; set; } = default!;
    public string? bairro { get; set; } = default!;
    public string? uf { get; set; } = default!;

    public string? telefone { get; set; }
    public string? email { get; set; }
}
