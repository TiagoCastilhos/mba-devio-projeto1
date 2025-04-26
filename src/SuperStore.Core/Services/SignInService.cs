using Microsoft.AspNetCore.Identity;
using SuperStore.Core.Abstractions.Services;
using SuperStore.Core.InputModels;
using SuperStore.Core.OutputModels;

namespace SuperStore.Core.Services;
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
            return new SignInOutputModel(false, false);

        var result = await _signInManager.PasswordSignInAsync(user, inputModel.Password, inputModel.IsPersistent, false);

        return new SignInOutputModel(result.Succeeded, true, result.IsLockedOut, result.IsNotAllowed);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
