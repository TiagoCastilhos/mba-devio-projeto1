using Microsoft.AspNetCore.Mvc;
using SuperStore.Authorization.Abstractions.Services;
using SuperStore.Authorization.Exceptions;
using SuperStore.Authorization.InputModels;
using SuperStore.Authorization.OutputModels;

namespace SuperStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IIdentitiesService _identitiesService;
    private readonly IUsersService _usersService;

    public AuthenticationController(IIdentitiesService identitiesService, IUsersService usersService)
    {
        _identitiesService = identitiesService;
        _usersService = usersService;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(UserOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] CreateUserInputModel inputModel, CancellationToken cancellationToken)
    {
        try
        {
            var userOutputModel = await _usersService.CreateAsync(inputModel, cancellationToken);
            return Ok(userOutputModel);
        }
        catch (UserLoginException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(LoginOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserInputModel inputModel, CancellationToken cancellationToken)
    {
        try
        {
            var loginOutputModel = await _identitiesService.LoginAsync(inputModel, cancellationToken);
            return Ok(loginOutputModel);
        }
        catch (UserLoginException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
