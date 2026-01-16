using BancoDoBrasil.Dtos.Boleto.Registrar;
using BancoDoBrasil.Exceptions;

public static class DescontoBusinesValidator
{
    public static void ValidarDesconto(RegistrarBoletoRequest descontos)
    {
        if(descontos == null || descontos.desconto == null)
            return;

        var d1 = descontos.desconto;
        var d2 = descontos.segundoDesconto;
        var d3 = descontos.terceiroDesconto;

        ValidarDesconto(d1, "primeiro");

        if(d2 != null)
        {
            ValidarDesconto(d2, "segundo");
            ValidarSequencia(d1, d2, "primeiro", "segundo");
        }

        if(d3 != null)
        {
            if(d2 == null)
                throw new BancoDoBrasilValidationException("Não é permitido terceiro desconto sem segundo desconto.");

            ValidarDesconto(d3, "terceiro");
            ValidarSequencia(d2, d3, "segundo", "terceiro");
        }

        // Tipos devem ser iguais
        if (d2 != null && d2.tipo != d1.tipo)
            throw new BancoDoBrasilValidationException("O tipo do segundo desconto deve ser igual ao do primeiro.");

        if (d3 != null && d3.tipo != d1.tipo)
            throw new BancoDoBrasilValidationException("O tipo do terceiro desconto deve ser igual ao do primeiro.");
    }

    private static void ValidarDesconto(DescontoDto d, string nome)
    {
        if(d.tipo is < 0 or > 2)
            throw new BancoDoBrasilValidationException($"Tipo inválido no {nome} desconto.");

        if (d.tipo == 0)
            throw new BancoDoBrasilValidationException($"Não é permitido definir {nome} desconto com tipo 0.");

        if (!d.dataExpiracao.HasValue)
            throw new BancoDoBrasilValidationException($"Data de expiração obrigatória no {nome} desconto.");

        if (d.tipo == 1 && !d.valor.HasValue)
            throw new BancoDoBrasilValidationException($"Valor obrigatório no {nome} desconto (tipo 1).");

        if (d.tipo == 2 && !d.porcentagem.HasValue)
            throw new BancoDoBrasilValidationException($"Porcentagem obrigatória no {nome} desconto (tipo 2).");
    }

    private static void ValidarSequencia( DescontoDto anterior, DescontoDto atual, string nomeAnterior,string nomeAtual)
    {
        if (atual.dataExpiracao <= anterior.dataExpiracao)
            throw new BancoDoBrasilValidationException(
                $"A data do {nomeAtual} desconto deve ser posterior à do {nomeAnterior}.");
    }
}