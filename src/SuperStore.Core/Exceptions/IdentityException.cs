namespace SuperStore.Core.Exceptions;

public abstract class IdentityException : Exception
{
    protected IdentityException(string message)
        : base(message)
    {
    }
}