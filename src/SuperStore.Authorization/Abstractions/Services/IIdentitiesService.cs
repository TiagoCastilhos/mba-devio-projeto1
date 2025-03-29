using SuperStore.Authorization.InputModels;
using SuperStore.Authorization.OutputModels;

namespace SuperStore.Authorization.Abstractions.Services;
public interface IIdentitiesService
{
    Task<LoginOutputModel> LoginAsync(LoginUserInputModel inputModel, CancellationToken cancellationToken);
}
