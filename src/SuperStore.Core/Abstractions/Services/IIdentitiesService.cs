using SuperStore.Core.InputModels;
using SuperStore.Core.OutputModels;

namespace SuperStore.Core.Abstractions.Services;
public interface IIdentitiesService
{
    Task<AuthTokenOutputModel> GenerateTokenAsync(UserSignInInputModel inputModel);
}
