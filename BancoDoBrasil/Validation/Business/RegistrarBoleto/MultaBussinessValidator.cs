using BancoDoBrasil.Dtos.Boleto.Registrar;
using BancoDoBrasil.Exceptions;

public static class MultaBussinessValidator
{
    public static void ValidarMulta(RegistrarBoletoRequest request)
    {
        var multa = request.multa;

        if(multa == null)
            return;

    ValidarTipo(multa);
    ValidarTipoZero(multa);
    ValidarDataObrigatoria(multa);
    ValidarRegraDatas(multa, request);
    ValidarValorQuandoTipoFixo(multa);
    ValidarPercentualQuandoTipoPercentual(multa);
    }

    private static void ValidarTipo(MultaDto multa)
    {
        if (multa.tipo < 0 || multa.tipo > 2)
            throw new ArgumentException("Tipo de multa inválido. Domínio permitido: 0, 1 ou 2.");
    }

    private static void ValidarTipoZero(MultaDto multa)
    {
        if (multa.tipo != 0)
            return;

        if (multa.data.HasValue || multa.valor.HasValue || multa.porcentagem.HasValue)
            throw new BancoDoBrasilValidationException(
                "Quando o tipo da multa for 0 (dispensar), nenhum outro campo deve ser informado.");
    }

    private static void ValidarDataObrigatoria(MultaDto multa)
    {
        if (multa.tipo is 1 or 2 && !multa.data.HasValue)
            throw new BancoDoBrasilValidationException(
                "A data da multa é obrigatória quando o tipo for 1 ou 2.");
    }

    private static void ValidarRegraDatas(
    MultaDto multa,
    RegistrarBoletoRequest request)
    {
        if (multa.tipo == 0)
            return;

        var dataVencimento = request.dataVencimento.Date;

        var dataLimiteRecebimento =
            request.numeroDiasLimiteRecebimento.HasValue
                ? dataVencimento.AddDays(request.numeroDiasLimiteRecebimento.Value)
                : (DateTime?)null;

        if (multa.data!.Value.Date <= dataVencimento)
            throw new BancoDoBrasilValidationException(
                "A data da multa deve ser posterior à data de vencimento do boleto.");

        if (dataLimiteRecebimento.HasValue &&
            multa.data.Value.Date > dataLimiteRecebimento.Value.Date)
            throw new BancoDoBrasilValidationException(
                "A data da multa deve ser anterior ou igual à data limite de recebimento do boleto vencido.");
    }

    private static void ValidarValorQuandoTipoFixo(MultaDto multa)
    {
        if (multa.tipo != 1)
            return;

        if (!multa.valor.HasValue)
            throw new BancoDoBrasilValidationException(
                "O valor da multa é obrigatório quando o tipo for 1 (valor fixo).");

        if (multa.porcentagem.HasValue)
            throw new BancoDoBrasilValidationException(
                "Não informe porcentagem quando o tipo da multa for 1.");
    }

    private static void ValidarPercentualQuandoTipoPercentual(MultaDto multa)
    {
        if (multa.tipo != 2)
            return;

        if (!multa.porcentagem.HasValue)
            throw new BancoDoBrasilValidationException(
                "A porcentagem da multa é obrigatória quando o tipo for 2 (percentual).");

        if (multa.valor.HasValue)
            throw new BancoDoBrasilValidationException(
                "Não informe valor quando o tipo da multa for 2.");
    }
}