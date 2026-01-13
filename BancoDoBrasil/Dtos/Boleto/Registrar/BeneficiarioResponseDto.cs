namespace BancoDoBrasil.Dtos.Boleto.Registrar;

public sealed class BeneficiarioResponseDto
{
    public int agencia { get; set; }
    public int contaCorrente { get; set; }
    public int tipoEndereco { get; set; }

    public string logradouro { get; set; } = default!;
    public string bairro { get; set; } = default!;
    public string cidade { get; set; } = default!;
    public int codigoCidade { get; set; }

    public string uf { get; set; } = default!;
    public int cep { get; set; }

    public string indicadorComprovacao { get; set; } = default!;
}
