namespace BancoDoBrasil.Exceptions;

public sealed class BancoDoBrasilValidationException : Exception
{
    public BancoDoBrasilValidationException(string message)
        : base(message) { }
}
