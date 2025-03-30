using Microsoft.AspNetCore.Identity;
using SuperStore.Authorization.Abstractions.Services;
using SuperStore.Authorization.InputModels;
using SuperStore.Authorization.OutputModels;

namespace SuperStore.Authorization.Services;
internal sealed class SignInService : ISignInService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public SignInService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<SignInOutputModel> SignInAsync(UserSignInInputModel inputModel)
    {
        var user = await _userManager.FindByEmailAsync(inputModel.Email);

        if (user == null)
        {
            return new SignInOutputModel
            {
                Succeeded = false,
                UserExists = false
            };
        }

        var result = await _signInManager.PasswordSignInAsync(user, inputModel.Password, true, false);

        return new SignInOutputModel
        {
            IsLockedOut = result.IsLockedOut,
            IsNotAllowed = result.IsNotAllowed,
            Succeeded = result.Succeeded,
            UserExists = true
        };
    }
}
