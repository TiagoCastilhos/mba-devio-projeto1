using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Authorization.Abstractions.Services;
using SuperStore.Authorization.Exceptions;
using SuperStore.Authorization.InputModels;
using SuperStore.MVC.ViewModels.Identities;

namespace SuperStore.MVC.Controllers;

[Route("Identities")]
public class IdentitiesController : Controller
{
    private readonly IValidator<UserSignInInputModel> _signInValidator;
    private readonly IValidator<CreateUserInputModel> _signUpValidator;
    private readonly ISignInService _signInService;
    private readonly IUsersService _usersService;

    public IdentitiesController(
        IValidator<UserSignInInputModel> signInValidator,
        IValidator<CreateUserInputModel> signUpValidator,
        ISignInService signInService,
        IUsersService usersService)
    {
        _signInValidator = signInValidator;
        _signUpValidator = signUpValidator;
        _signInService = signInService;
        _usersService = usersService;
    }

    [HttpGet("SignIn")]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignInAsync(SignInViewModel signInViewModel)
    {
        var inputModel = new UserSignInInputModel(signInViewModel.Email, signInViewModel.Password, signInViewModel.RememberMe);

        var validationResult = await _signInValidator.ValidateAsync(inputModel);

        if (!validationResult.IsValid)
        {
            signInViewModel.SetErrors(validationResult.ToDictionary());
            return View(signInViewModel);
        }

        var signInOutputModel = await _signInService.SignInAsync(inputModel);

        if (signInOutputModel.Succeeded)
            return RedirectToAction("Index", "Home");

        //TODO: Service could return the errors instead of the bools
        if (signInOutputModel.IsLockedOut)
            signInViewModel.AddError("Email", "Usuário está bloqueado. Tente novamente mais tarde.");

        if (!signInOutputModel.UserExists)
            signInViewModel.AddError("Email", "Usuário não existe.");

        if (signInOutputModel.IsNotAllowed)
            signInViewModel.AddError("Email", "Usuário não tem permissão.");

        if (signInOutputModel.PasswordIsIncorrect)
            signInViewModel.AddError("Password", "Senha incorreta.");

        return View(signInViewModel);
    }

    [HttpGet("SignUp")]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUpAsync(SignUpViewModel signInViewModel)
    {
        var inputModel = new CreateUserInputModel(signInViewModel.Email, signInViewModel.Name, signInViewModel.Password);

        var validationResult = await _signUpValidator.ValidateAsync(inputModel);

        if (!validationResult.IsValid)
        {
            signInViewModel.SetErrors(validationResult.ToDictionary());
            return View(signInViewModel);
        }

        try
        {
            await _usersService.CreateAsync(inputModel, Request.HttpContext.RequestAborted);
        }
        catch (UserCreationException ex)
        {
            AddUserCreationErrors(signInViewModel, ex);

            return View(signInViewModel);
        }

        var signInInputModel = new UserSignInInputModel(signInViewModel.Email, signInViewModel.Password, true);

        await _signInService.SignInAsync(signInInputModel);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("SignOut")]
    public async Task<IActionResult> SignOutAsync()
    {
        await _signInService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    private static void AddUserCreationErrors(SignUpViewModel signInViewModel, UserCreationException ex)
    {
        foreach (var error in ex.Errors)
        {
            if (error.StartsWith("Email"))
                signInViewModel.AddError("Email", error);

            if (error.StartsWith("Username"))
                signInViewModel.AddError("Name", error);
        }
    }
}
