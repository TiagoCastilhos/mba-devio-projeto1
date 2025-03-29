using Microsoft.AspNetCore.Identity;

namespace SuperStore.Authorization.Exceptions;
public sealed class UserCreationException : IdentityException
{
    public UserCreationException(IEnumerable<IdentityError> errors)
        : base(string.Join(", ", errors.Select(e => e.Description)))
    {
    }
}
