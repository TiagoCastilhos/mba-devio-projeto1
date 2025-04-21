using SuperStore.Core.InputModels;
using SuperStore.Core.OutputModels;

namespace SuperStore.Core.Abstractions.Services;

public interface IUsersService
{
    Task<UserOutputModel> CreateAsync(CreateUserInputModel inputModel, CancellationToken cancellationToken);
}
