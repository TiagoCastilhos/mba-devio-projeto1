using SuperStore.Authorization.InputModels;

namespace SuperStore.Authorization.Abstractions.Services;
public interface IUsersService
{
    Task CreateUserAsync(CreateUserInputModel inputModel, CancellationToken cancellationToken);
}
