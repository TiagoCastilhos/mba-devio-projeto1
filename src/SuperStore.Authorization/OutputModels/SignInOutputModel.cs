namespace SuperStore.Authorization.OutputModels;

public sealed class SignInOutputModel
{
    public required bool Succeeded { get; init; }
    public required bool UserExists { get; init; }
    public bool IsLockedOut { get; init; }
    public bool IsNotAllowed { get; init; }
    public bool PasswordIsIncorrect => !Succeeded && UserExists && !IsLockedOut && !IsNotAllowed;
}