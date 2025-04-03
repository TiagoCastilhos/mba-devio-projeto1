using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SuperStore.Application.Services;

internal abstract class ServiceBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    protected ServiceBase(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null)
            return null;

        return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
