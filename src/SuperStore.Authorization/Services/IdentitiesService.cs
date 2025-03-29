using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SuperStore.Authorization.Abstractions.Services;
using SuperStore.Authorization.Exceptions;
using SuperStore.Authorization.InputModels;
using SuperStore.Authorization.OutputModels;
using IdentityOptions = SuperStore.Authorization.Options.IdentityOptions;

namespace SuperStore.Authorization.Services;
internal sealed class IdentitiesService : IIdentitiesService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IdentityOptions _identityOptions;

    public IdentitiesService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IdentityOptions identityOptions)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _identityOptions = identityOptions;
    }

    public async Task<LoginOutputModel> LoginAsync(LoginUserInputModel inputModel, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(inputModel.Email)
            ?? throw new UserLoginException("Could not authenticate user. Verify the provided credentials and try again");

        var result = await _signInManager.PasswordSignInAsync(user, inputModel.Password, true, false);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
                throw new UserLoginException("User is locked out");

            throw new UserLoginException("Could not authenticate user. Verify the provided credentials and try again");
        }

        return new LoginOutputModel
        {
            AccessToken = GenerateJwtToken(user)
        };
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var key = Encoding.ASCII.GetBytes(_identityOptions.SigningKey);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new(JwtRegisteredClaimNames.Iss, _identityOptions.Issuer),
                new(JwtRegisteredClaimNames.Aud, _identityOptions.Audience),
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Role, "Seller"), //Check if user is a seller, currently every user is a seller
                new(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
                new(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64),
            ]),
            Expires = DateTime.UtcNow.AddHours(_identityOptions.TokenExpirationInHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        return jwtSecurityTokenHandler.WriteToken(token);
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - DateTime.UnixEpoch).TotalSeconds);
}
