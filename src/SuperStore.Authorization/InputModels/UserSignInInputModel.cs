namespace SuperStore.Authorization.InputModels;

public sealed record UserSignInInputModel(string Email, string Password, bool IsPersistent);