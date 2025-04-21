using Microsoft.AspNetCore.Identity;
using SuperStore.Core.Abstractions.Services;
using SuperStore.Core.Exceptions;
using SuperStore.Core.InputModels;
using SuperStore.Core.OutputModels;

namespace SuperStore.Core.Services;
internal sealed class UsersService : IUsersService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ISellersService _sellersService;

    public UsersService(UserManager<IdentityUser> userManager, ISellersService sellersService)
    {
        _userManager = userManager;
        _sellersService = sellersService;
    }

    public async Task<UserOutputModel> CreateAsync(CreateUserInputModel inputModel, CancellationToken cancellationToken)
    {
        var user = new IdentityUser
        {
            UserName = inputModel.Name,
            Email = inputModel.Email
        };

        var result = await _userManager.CreateAsync(user, inputModel.Password);

        if (!result.Succeeded)
            throw new UserCreationException(result.Errors);

        var createdUser = await _userManager.FindByEmailAsync(inputModel.Email);

        await _sellersService.CreateAsync(new CreateSellerInputModel(inputModel.Name, createdUser!.Id), cancellationToken);

        return new UserOutputModel(createdUser!);
    }
}
