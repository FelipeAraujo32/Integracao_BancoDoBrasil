using System.Text.Json.Serialization;

namespace BancoDoBrasil.Dtos.Boleto.Registrar;

public sealed class RegistrarBoletoRequest
{
    public int numeroConvenio { get; set; }
    public int numeroCarteira { get; set; }
    public int numeroVariacaoCarteira { get; set; }
    public int codigoModalidade { get; set; }

    public DateTime dataEmissao { get; set; } = default!;
    public DateTime dataVencimento { get; set; } = default!;

    public decimal valorOriginal { get; set; }
    public decimal? valorAbatimento { get; set; }

    public int? quantidadeDiasProtesto { get; set; }
    public int? quantidadeDiasNegativacao { get; set; }
    public int? orgaoNegativador { get; set; }

    public string? indicadorAceiteTituloVencido { get; set; }
    public int? numeroDiasLimiteRecebimento { get; set; }

    public string codigoAceite { get; set; } = default!;
    public int codigoTipoTitulo { get; set; }
    public string descricaoTipoTitulo { get; set; } = default!;

    public string indicadorPermissaoRecebimentoParcial { get; set; } = default!;

    public string numeroTituloBeneficiario { get; set; } = default!;
    public string? campoUtilizacaoBeneficiario { get; set; }
    public string? numeroTituloCliente { get; set; }
    
    [JsonIgnore]
    public long numeroControle { get; set; }
    public string? mensagemBloquetoOcorrencia { get; set; }

    public DescontoDto? desconto { get; set; }
    public DescontoDto? segundoDesconto { get; set; }
    public DescontoDto? terceiroDesconto { get; set; }

    public JurosMoraDto? jurosMora { get; set; }
    public MultaDto? multa { get; set; }

    public PagadorDto pagador { get; set; } = default!;
    public BeneficiarioFinalDto? beneficiarioFinal { get; set; }

    public string indicadorPix { get; set; } = default!;
}
