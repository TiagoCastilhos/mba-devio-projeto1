using Microsoft.AspNetCore.Identity;

namespace SuperStore.Core.Exceptions;
public sealed class UserCreationException : IdentityException
{
    public IReadOnlyDictionary<string, List<string>> Errors { get; }

    public UserCreationException(IEnumerable<IdentityError> identityErrors)
        : base(string.Join(", ", identityErrors.Select(e => e.Description)))
    {
        var errors = new Dictionary<string, List<string>>();

        foreach (var error in identityErrors)
        {
            var key = error.Code.Contains("Name") ? "Name"
                : error.Code.Contains("Email") ? "Email"
                : error.Code;

            if (!errors.ContainsKey(key))
                errors[key] = [];

            errors[key].Add(error.Description);
        }

        Errors = errors;
    }
}
