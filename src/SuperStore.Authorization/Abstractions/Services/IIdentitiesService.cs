﻿using SuperStore.Authorization.InputModels;
using SuperStore.Authorization.OutputModels;

namespace SuperStore.Authorization.Abstractions.Services;
public interface IIdentitiesService
{
    Task<AuthTokenOutputModel> GenerateTokenAsync(UserSignInInputModel inputModel);
}
