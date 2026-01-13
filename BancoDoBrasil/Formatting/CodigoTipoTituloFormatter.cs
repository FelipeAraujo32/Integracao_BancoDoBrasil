using BancoDoBrasil.Exceptions;

namespace BancoDoBrasil.Formatting;

public static class CodigoTipoTituloFormatter
{
    public static string ObterDescricao(int codigoTipoTitulo)
    {
        return codigoTipoTitulo switch
        {
            1  => "CH", // Cheque
            2  => "DM", // Duplicata Mercantil
            4  => "DS", // Duplicata de Serviço
            6  => "DR", // Duplicata Rural
            7  => "LC", // Letra de Câmbio
            12 => "NP", // Nota Promissória
            17 => "RC", // Recibo
            19 => "ND", // Nota de Débito
            26 => "WR", // Warrant
            27 => "DE", // Dívida Ativa Estado
            28 => "DM", // Dívida Ativa Município
            29 => "DU", // Dívida Ativa União
            31 => "CC", // Cartão de Crédito
            32 => "BP", // Boleto Proposta
            33 => "BA", // Boleto Aporte
            _  => throw new BancoDoBrasilValidationException(
                "codigoTipoTitulo inválido.")
        };
    }
}
