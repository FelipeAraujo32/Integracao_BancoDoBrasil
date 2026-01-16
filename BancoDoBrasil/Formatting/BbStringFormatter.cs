namespace BancoDoBrasil.Formatting;

internal static class BbStringFormatter
{
    public static string NormalizeUpper(string? value, int max)
    {
        var v = (value ?? string.Empty).Trim().ToUpperInvariant();
        return v.Length > max ? v[..max] : v;
    }

    public static string NormalizarMensagemBloqueto (string? mensagem)
    {
        if (string.IsNullOrWhiteSpace(mensagem))
        return string.Empty;

        mensagem = mensagem.Replace("\r", " ").Replace("\n", " ");
        mensagem = mensagem.Trim();

        if(mensagem.Length > 165)
            throw new ArgumentException(
                 "A mensagem do bloqueto não pode ultrapassar 165 caracteres.");
        return mensagem;
    }
}