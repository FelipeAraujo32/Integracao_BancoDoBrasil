namespace BancoDoBrasil.Exceptions;

public sealed class BancoDoBrasilIntegrationException : Exception
{
    public BancoDoBrasilIntegrationException(string message)
        : base(message) { }
}
