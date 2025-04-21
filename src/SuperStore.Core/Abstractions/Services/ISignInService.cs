using SuperStore.Core.InputModels;
using SuperStore.Core.OutputModels;

namespace SuperStore.Core.Abstractions.Services;

public interface ISignInService
{
    Task<SignInOutputModel> SignInAsync(UserSignInInputModel inputModel);
    Task SignOutAsync();
}