using BancoDoBrasil.Dtos.Boleto.Registrar;
using BancoDoBrasil.Exceptions;

namespace BancoDoBrasil.Validation.Structure;

internal static class RegistrarBoletoStructureValidator
{
    public static void Validate(RegistrarBoletoRequest r)
    {
       

        if (r.numeroVariacaoCarteira <= 0)
            throw new BancoDoBrasilValidationException("numeroVariacaoCarteira é obrigatório");

        if (r.valorOriginal <= 0)
            throw new BancoDoBrasilValidationException("valorOriginal deve ser maior que zero");

        if (string.IsNullOrWhiteSpace(r.numeroTituloBeneficiario))
            throw new BancoDoBrasilValidationException("numeroTituloBeneficiario é obrigatório");

        if (r.pagador == null)
            throw new BancoDoBrasilValidationException("pagador é obrigatório");
    }
}
