using Microsoft.AspNetCore.Identity;
using SuperStore.Authorization.Abstractions.Services;
using SuperStore.Authorization.InputModels;
using SupperStore.Application.Abstractions.Services;
using SupperStore.Application.InputModels;

namespace SuperStore.Authorization.Services;
internal sealed class UsersService : IUsersService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ISellersService _sellersService;

    public UsersService(UserManager<IdentityUser> userManager, ISellersService sellersService)
    {
        _userManager = userManager;
        _sellersService = sellersService;
    }

    public async Task CreateUserAsync(CreateUserInputModel inputModel, CancellationToken cancellationToken)
    {
        var user = new IdentityUser
        {
            UserName = inputModel.Name,
            Email = inputModel.Email
        };

        var result = await _userManager.CreateAsync(user, inputModel.Password);

        if (!result.Succeeded)
        {
            //throw exception
        }

        var createdUser = await _userManager.FindByEmailAsync(inputModel.Name);
        
        var seller = await _sellersService.CreateAsync(new CreateSellerInputModel(inputModel.Name, createdUser!.Id), cancellationToken);
    }
}
