namespace SuperStore.Authorization.InputModels;
public sealed record CreateUserInputModel(string Email, string Name, string Password);