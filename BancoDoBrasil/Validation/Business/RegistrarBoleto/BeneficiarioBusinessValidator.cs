using System.Text.RegularExpressions;
using BancoDoBrasil.Dtos.Boleto.Registrar;
using BancoDoBrasil.Exceptions;

public static class BeneficiarioBusinessValidator
{
    public static void ValidarBeneficiario(BeneficiarioFinalDto? beneficiario)
    {
        if (beneficiario == null)
                return;

        bool algumCampoPreenchido =
        beneficiario.tipoInscricao.HasValue ||
        beneficiario.numeroInscricao.HasValue ||
        !string.IsNullOrWhiteSpace(beneficiario.nome);

        if (!algumCampoPreenchido)
                return;

        ValidarTipoInscricao(beneficiario.tipoInscricao);
        ValidarNumeroInscricao(beneficiario.tipoInscricao, beneficiario.numeroInscricao);
        ValidarNome(beneficiario.nome);
        
    }

     private static void ValidarTipoInscricao(int? tipoInscricao)
        {
            if(tipoInscricao == null)
                return;
                
            if (tipoInscricao != 1 && tipoInscricao != 2)
                throw new BancoDoBrasilValidationException(
                    "Tipo de inscrição inválido. Domínio: 1 (Pessoa Física) ou 2 (Pessoa Jurídica).");
        }

    private static void ValidarNumeroInscricao(int? tipoInscricao, long? numeroInscricao)
        {
            if (!numeroInscricao.HasValue)
                throw new BancoDoBrasilValidationException(
                    "Número de inscrição do beneficiário final é obrigatório.");

            if (!tipoInscricao.HasValue)
                return; // segurança, já validado antes

            int quantidadeDigitos = numeroInscricao.Value
                .ToString()
                .Length;

            if (tipoInscricao == 1 && quantidadeDigitos != 11)
                throw new BancoDoBrasilValidationException(
                    "CPF do beneficiário final deve conter exatamente 11 dígitos.");

            if (tipoInscricao == 2 && quantidadeDigitos != 14)
                throw new BancoDoBrasilValidationException(
                    "CNPJ do beneficiário final deve conter exatamente 14 dígitos.");
        }

    private static void ValidarNome(string? nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new BancoDoBrasilValidationException(
                    "Nome do beneficiário final é obrigatório quando informado.");

            if (nome.Length > 30)
                throw new BancoDoBrasilValidationException(
                    "Nome do beneficiário final deve ter no máximo 30 caracteres.");
        }   
}