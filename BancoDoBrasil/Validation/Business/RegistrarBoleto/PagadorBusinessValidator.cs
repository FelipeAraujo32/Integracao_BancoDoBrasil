using BancoDoBrasil.Dtos.Boleto.Registrar;
using BancoDoBrasil.Exceptions;

public static class PagadorBusinessValidator
{
    public static void ValidarPagador(RegistrarBoletoRequest request)
    {
        if(request.pagador == null)
            throw new BancoDoBrasilValidationException("Pagador é obrigatório.");

        ValidarDocumento(request.pagador);
        ValidarTamanhoCaracteres(request.pagador.nome, 60);
        ValidarTamanhoCaracteres(request.pagador.endereco, 60);
        validarCep(request.pagador);
        ValidarTamanhoCaracteres(request.pagador.cidade, 30);
        ValidarTamanhoCaracteres(request.pagador.bairro, 30);
        ValidarUf(request.pagador);
        ValidarTamanhoCaracteres(request.pagador.telefone, 30);

    }

    private static void ValidarDocumento(PagadorDto p)
    {
        var doc = p.numeroInscricao.ToString();

        if(doc.Length == 11)
        {
            if(p.tipoInscricao != 1)
                 throw new BancoDoBrasilValidationException(
                    "Tipo de inscrição inconsistente: documento é CPF, mas tipoInscricao não é 1.");
        }
        else if (doc.Length == 14)
        {
            if (p.tipoInscricao != 2)
                throw new BancoDoBrasilValidationException(
                    "Tipo de inscrição inconsistente: documento é CNPJ, mas tipoInscricao não é 2.");
        }
        else
        {
            throw new BancoDoBrasilValidationException(
                "numeroInscricao deve conter 11 (CPF) ou 14 (CNPJ) dígitos.");
        }
    }

    private static void ValidarTamanhoCaracteres(string? p, int max)
    {
        if (!string.IsNullOrEmpty(p) && p.Length > max)
            throw new BancoDoBrasilValidationException(
                $"{p} máximo: {max} caracteres."
            );
    }

    private static void validarCep(PagadorDto p)
    {
        if (p.cep <=0)
            throw new ArgumentException("CEP inválido.");
    
    }

    private static void ValidarUf(PagadorDto p)
    {
        if (string.IsNullOrEmpty(p.uf) || p.uf.Length != 2)
            throw new ArgumentException("UF deve ter exatamente 2 caracteres.");

        if (!EstadosValidos.Contains(p.uf.ToUpperInvariant()))
            throw new ArgumentException($"UF inválida: {p.uf}.");    
    }

    private static readonly HashSet<string> EstadosValidos = new()
    {
        "AC","AL","AP","AM","BA","CE","DF","ES","GO","MA",
        "MT","MS","MG","PA","PB","PR","PE","PI","RJ","RN",
        "RS","RO","RR","SC","SP","SE","TO"
    };
}
