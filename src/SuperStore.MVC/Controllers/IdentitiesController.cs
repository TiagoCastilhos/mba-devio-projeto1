using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Core.Abstractions.Services;
using SuperStore.Core.Exceptions;
using SuperStore.Core.InputModels;
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

        signInViewModel.SetErrors(signInOutputModel.Errors);

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
            signInViewModel.SetErrors(ex.Errors);

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
}
