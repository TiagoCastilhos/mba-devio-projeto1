using System.Text.RegularExpressions;
using FluentValidation;

namespace SuperStore.Authorization.InputModels.Validators;

public sealed class CreateUserInputModelValidator : AbstractValidator<CreateUserInputModel>
{
    private static Regex _userNameRegex = new(@"^[a-zA-Z0-9\-._@+]+$", RegexOptions.Compiled);
    private static Regex _passwordRegex = new(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])(?=.{6,})(?:(.)(?!.*\1)){6,}$", RegexOptions.Compiled);

    public CreateUserInputModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50)
            .Must(_userNameRegex.IsMatch)
            .WithMessage("Nome do usuário pode conter apenas letras, números e os seguintes caracteres especiais: -._@+");

        RuleFor(x => x.Password)
            .NotEmpty()
            .Must(_passwordRegex.IsMatch)
            .WithMessage("Senha precisa conter no mínimo 6 caracteres, 1 número, 1 letra maiúscula, 1 letra minúscula e 1 caracter especial");
    }
}
