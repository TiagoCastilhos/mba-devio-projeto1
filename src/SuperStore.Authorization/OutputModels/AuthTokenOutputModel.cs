namespace SuperStore.Authorization.OutputModels;

public sealed class AuthTokenOutputModel
{
    public required string AccessToken { get; init; }
}
