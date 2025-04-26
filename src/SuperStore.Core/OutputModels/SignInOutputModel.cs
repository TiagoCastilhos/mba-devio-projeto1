namespace SuperStore.Core.OutputModels;

public sealed class SignInOutputModel
{
    public bool Succeeded { get; }
    public IReadOnlyDictionary<string, List<string>>? Errors { get; }

    public SignInOutputModel(bool succeeded, bool userExists, bool isLockedOut = false, bool isNotAllowed = false)
    {
        Succeeded = succeeded;

        var errors = new Dictionary<string, List<string>>();

        if (succeeded)
            return;

        if (!userExists)
            AddError("Email", "Usuário não existe.", errors);

        if (isLockedOut)
            AddError("Email", "Usuário está bloqueado. Tente novamente mais tarde.", errors);

        if (isNotAllowed)
            AddError("Email", "Usuário não tem permissão.", errors);

        if (userExists && !isLockedOut && !isNotAllowed)
            AddError("Password", "Senha incorreta.", errors);

        Errors = errors;
    }

    private static void AddError(string fieldName, string error, Dictionary<string, List<string>> errors)
    {
        if (!errors.ContainsKey(fieldName))
            errors[fieldName] = [];

        errors[fieldName].Add(error);
    }
}