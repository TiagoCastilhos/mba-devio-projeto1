using Microsoft.AspNetCore.Identity;

namespace SuperStore.Core.Exceptions;
public sealed class UserCreationException : IdentityException
{
    public IEnumerable<string> Errors { get; }

    public UserCreationException(IEnumerable<IdentityError> errors)
        : base(string.Join(", ", errors.Select(e => e.Description)))
    {
        Errors = errors.Select(e => e.Description);
    }
}
