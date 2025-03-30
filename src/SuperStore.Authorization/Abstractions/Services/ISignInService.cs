using SuperStore.Authorization.InputModels;
using SuperStore.Authorization.OutputModels;

namespace SuperStore.Authorization.Abstractions.Services;

public interface ISignInService
{
    Task<SignInOutputModel> SignInAsync(UserSignInInputModel inputModel);
}