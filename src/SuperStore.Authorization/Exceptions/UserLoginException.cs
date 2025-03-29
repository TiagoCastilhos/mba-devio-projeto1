namespace SuperStore.Authorization.Exceptions;

public sealed class UserLoginException : IdentityException
{
    public UserLoginException(string message)
        : base(message)
    {
    }
}