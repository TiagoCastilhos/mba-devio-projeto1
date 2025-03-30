namespace SuperStore.Authorization.Exceptions;

public sealed class UserSignInException : IdentityException
{
    public UserSignInException(string message)
        : base(message)
    {
    }
}