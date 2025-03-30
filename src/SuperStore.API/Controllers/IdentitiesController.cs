using Microsoft.AspNetCore.Mvc;
using SuperStore.Authorization.Abstractions.Services;
using SuperStore.Authorization.Exceptions;
using SuperStore.Authorization.InputModels;
using SuperStore.Authorization.OutputModels;

namespace SuperStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentitiesController : ControllerBase
{
    private readonly IIdentitiesService _identitiesService;
    private readonly IUsersService _usersService;

    public IdentitiesController(IIdentitiesService identitiesService, IUsersService usersService)
    {
        _identitiesService = identitiesService;
        _usersService = usersService;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(UserOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] CreateUserInputModel inputModel)
    {
        try
        {
            var userOutputModel = await _usersService.CreateAsync(inputModel, Request.HttpContext.RequestAborted);
            return Ok(userOutputModel);
        }
        catch (UserSignInException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("signin")]
    [ProducesResponseType(typeof(AuthTokenOutputModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignInAsync([FromBody] UserSignInInputModel inputModel)
    {
        try
        {
            var authTokenOutputModel = await _identitiesService.GenerateTokenAsync(inputModel);
            return Ok(authTokenOutputModel);
        }
        catch (UserSignInException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
