using System.Text.RegularExpressions;
using FluentValidation;

namespace SuperStore.Core.InputModels.Validators;

public sealed class CreateUserInputModelValidator : AbstractValidator<CreateUserInputModel>
{
    public static Regex UserNameRegex = new(@"^[a-zA-Z0-9\-._@+]+$", RegexOptions.Compiled);
    public static Regex PasswordRegex = new(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])(?=.{6,})(?:(.)(?!.*\1)){6,}$", RegexOptions.Compiled);

    public CreateUserInputModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50)
            .Must(UserNameRegex.IsMatch)
            .WithMessage("Nome do usuário pode conter apenas letras, números e os seguintes caracteres especiais: -._@+");

        RuleFor(x => x.Password)
            .NotEmpty()
            .Must(PasswordRegex.IsMatch)
            .WithMessage("Senha precisa conter no mínimo 6 caracteres, 1 número, 1 letra maiúscula, 1 letra minúscula e 1 caracter especial");
    }
}
