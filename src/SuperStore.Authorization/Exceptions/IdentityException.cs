namespace SuperStore.Authorization.Exceptions;

public abstract class IdentityException : Exception
{
    protected IdentityException(string message)
        : base(message)
    {
    }
}