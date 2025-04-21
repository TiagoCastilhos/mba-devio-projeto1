using Microsoft.AspNetCore.Identity;

namespace SuperStore.Core.OutputModels;
public sealed class UserOutputModel(IdentityUser user)
{
    public string Id { get; } = user.Id;
    public string Name { get; } = user.UserName!;
    public string Email { get; } = user.Email!;
}
