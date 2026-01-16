using System.Globalization;
using System.Text.RegularExpressions;
using BancoDoBrasil.Dtos.Boleto.Registrar;
using BancoDoBrasil.Exceptions;

namespace BancoDoBrasil.Validation.Business;

public static class RegistrarBoletoBusinessValidator
{
    public static void Validate(RegistrarBoletoRequest r)
    {
        ValidarNumeroConvenio(r);
        ValidarNumeroMaiorZero(r.numeroCarteira, "NumeroCarteira deve ser maior que zero");
        ValidarNumeroMaiorZero(r.numeroVariacaoCarteira, "NumeroVariacaoCarteira deve ser maior que zero");
        ValidarCodigoModalidade(r);
        ValidarDataEmissao(r);
        ValidarDataVencimento(r);
        ValidarValorOriginal(r.valorOriginal, r.desconto?.valor, r.segundoDesconto?.valor, r.terceiroDesconto?.valor, r.valorAbatimento);
        ValidarValorAbatimento(r.valorAbatimento);
        ValidarQtdDiasProtesto(r.quantidadeDiasProtesto);
        ValidarIndicadorAceite(r.indicadorAceiteTituloVencido);
        ValidarNumeroDiasLimiteRecebimento(r.indicadorAceiteTituloVencido, r.numeroDiasLimiteRecebimento);
        ValidarCodigoAceite(r.codigoAceite);
        ValidarCodigoTipoTitulo(r.codigoTipoTitulo, r.numeroCarteira);
        ValidarIndicadorPermissaoRecebimentoParcial(r.indicadorPermissaoRecebimentoParcial);
        ValidarNumeroTituloBeneficiario(r.numeroTituloBeneficiario);
        ValidarCampoUtilizacaoBeneficiario(r.campoUtilizacaoBeneficiario);
        ValidarNumeroControle(r.numeroControle);    
    }

    private static void ValidarNumeroConvenio(RegistrarBoletoRequest request)
    {   
        if (request.numeroConvenio < 0)
        throw new BancoDoBrasilValidationException("Número do convênio não pode ser negativo.");

        if (request.numeroConvenio > 9_999_999)
            throw new BancoDoBrasilValidationException(
                "Número do convênio deve ter no máximo 7 dígitos.");
    }

    private static void ValidarNumeroMaiorZero(int number, String text)
    {
        if (number <= 0)
            throw new BancoDoBrasilValidationException(text);
    }
    private static void ValidarCodigoModalidade(RegistrarBoletoRequest request)
    {
        if (request.codigoModalidade != 1 && request.codigoModalidade != 4)
            throw new BancoDoBrasilValidationException("codigoModalidade inválido");
    }

    private static void ValidarDataEmissao(RegistrarBoletoRequest request)
    {
        if (request.dataEmissao.Date < DateTime.Today)
            throw new BancoDoBrasilValidationException(
                "dataEmissao precisa ser igual ou maior que a data atual");
        }

    private static void ValidarDataVencimento(RegistrarBoletoRequest request)
    {
        if (request.dataVencimento.Date < request.dataEmissao.Date)
            throw new BancoDoBrasilValidationException(
                "dataVencimento não pode ser menor que dataEmissao");
    }

    private static void ValidarValorOriginal(
        decimal valorOriginal,
        decimal? desconto1,
        decimal? desconto2,
        decimal? desconto3,
        decimal? valorAbatimento)
    {
        if (valorOriginal <= 0)
            throw new BancoDoBrasilValidationException(
                "valorOriginal deve ser maior que zero.");

        var somaDescontos =
            (desconto1 ?? 0m) +
            (desconto2 ?? 0m) +
            (desconto3 ?? 0m);

        var totalReducao = somaDescontos + (valorAbatimento ?? 0m);

        if (valorOriginal <= totalReducao)
            throw new BancoDoBrasilValidationException(
                "valorOriginal deve ser maior que a soma dos descontos e abatimento.");
    }

    private static void ValidarValorAbatimento(decimal? valorAbatimento)
    {
        if (valorAbatimento.HasValue && valorAbatimento.Value <= 0)
            throw new BancoDoBrasilValidationException(
                "valorAbatimento, se informado, precisa ser maior que zero.");
    }
    
     private static void ValidarQtdDiasProtesto(int? dias)
    {
        if(dias == 0)
            return;

        bool valido =
            (dias >= 3 && dias <= 29) ||
            dias == 35 ||
            dias == 40 ||
            dias == 45;

        if (!valido)
        throw new BancoDoBrasilValidationException(
            "quantidadeDiasProtesto inválida. Use 0, 3 a 29, 35, 40 ou 45.");
    }

    private static void ValidarIndicadorAceite(string? indicador)
    {
        if (string.IsNullOrWhiteSpace(indicador))
            return; // usa regra do convênio

        indicador = indicador.Trim().ToUpperInvariant();
        
        if(indicador != "S" && indicador != "N")
            throw new BancoDoBrasilValidationException("indicadorAceiteTituloVencido deve ser 'S' ou 'N'.");
    }

    private static void ValidarNumeroDiasLimiteRecebimento(
    string? indicadorAceite,
    int? numeroDiasLimiteRecebimento)
    {
        if (numeroDiasLimiteRecebimento < 0)
            throw new BancoDoBrasilValidationException(
                "numeroDiasLimiteRecebimento deve ser 0 ou maior que zero.");

        // só pode ter dias > 0 se aceitar vencido
        if (numeroDiasLimiteRecebimento > 0 && indicadorAceite != "S")
            throw new BancoDoBrasilValidationException(
                "numeroDiasLimiteRecebimento só pode ser maior que zero quando indicadorAceiteTituloVencido = 'S'.");
    }
    private static void ValidarCodigoAceite(string? codigoAceite)
    {
        if(string.IsNullOrWhiteSpace(codigoAceite))
            throw new BancoDoBrasilValidationException(
                "codigoAceite é obrigatório e deve ser 'A' ou 'N'.");

        codigoAceite = codigoAceite.Trim().ToUpperInvariant();        

        if (codigoAceite != "A" && codigoAceite != "N")
            throw new BancoDoBrasilValidationException(
                "codigoAceite deve ser 'A' (Aceito) ou 'N' (Não aceito).");

        
    }
    private static void ValidarCodigoTipoTitulo(int codigoTipoTitulo, int numeroCarteira)
    {
        bool valido;

        if(numeroCarteira == 17)
        {
            valido = codigoTipoTitulo is
                1 or 2 or 4 or 6 or 7 or 12 or 17 or 19 or
                26 or 27 or 28 or 29 or 31 or 32 or 33;
        }
        else if (numeroCarteira is 11 or 17) //modalidade vinculada
        {
            valido = codigoTipoTitulo is 2 or 4;
        }
        else
        {
            // se a carteira não estiver mapeada, não validamos aqui
            return;
        };

        if(!valido)
            throw new BancoDoBrasilValidationException(
                $"codigoTipoTitulo inválido para a carteira {numeroCarteira}.");
    }
    private static void ValidarIndicadorPermissaoRecebimentoParcial(string? indicador)
    {
        if (string.IsNullOrWhiteSpace(indicador))
            throw new BancoDoBrasilValidationException(
                "indicadorPermissaoRecebimentoParcial é obrigatório e deve ser 'S' ou 'N'.");

         indicador = indicador.Trim().ToUpperInvariant();

        if (indicador != "S" && indicador != "N")
            throw new BancoDoBrasilValidationException(
                "indicadorPermissaoRecebimentoParcial deve ser 'S' ou 'N'.");        
    }
    private static void ValidarNumeroTituloBeneficiario(string numeroTituloBeneficiario)
    {
        if(string.IsNullOrWhiteSpace(numeroTituloBeneficiario))
            throw new BancoDoBrasilValidationException(
                "numeroTituloBeneficiario é obrigatório.");

         if (numeroTituloBeneficiario.Length > 15)
            throw new BancoDoBrasilValidationException(
                "numeroTituloBeneficiario deve ter no máximo 15 caracteres.");

        var regex = new Regex(@"^[A-Za-z0-9 \-']+$");
        
        if (!regex.IsMatch(numeroTituloBeneficiario))
            throw new BancoDoBrasilValidationException(
                "numeroTituloBeneficiario contém caracteres inválidos.");
    }
  
    private static void ValidarCampoUtilizacaoBeneficiario(string? campoUtilizacaoBeneficiario)
    {
        if (!string.IsNullOrEmpty(campoUtilizacaoBeneficiario) && campoUtilizacaoBeneficiario.Length > 25)
            throw new BancoDoBrasilValidationException(
                $"{campoUtilizacaoBeneficiario} máximo: 25 caracteres."
            );
    }
    private static void ValidarNumeroControle(long numeroControle)
    {
        if (numeroControle <= 0)
        throw new BancoDoBrasilValidationException(
            "NumeroControle deve ser maior que zero");

        if (numeroControle > 9999999999)
            throw new BancoDoBrasilValidationException(
                "NumeroControle deve ter no máximo 10 dígitos");
    }
    
    
}