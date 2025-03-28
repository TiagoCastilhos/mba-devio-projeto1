﻿using SuperStore.Authorization.InputModels;
using SuperStore.Authorization.OutputModels;

namespace SuperStore.Authorization.Abstractions.Services;

public interface IUsersService
{
    Task<UserOutputModel> CreateAsync(CreateUserInputModel inputModel, CancellationToken cancellationToken);
}
