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
            .WithMessage("Email é obrigatório")
            .EmailAddress()
            .WithMessage("Email inválido");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nome do usuário é obrigatório")
            .MaximumLength(50)
            .WithMessage("Nome do usuário deve ter no máximo 50 caracteres")
            .Must(UserNameRegex.IsMatch)
            .WithMessage("Nome do usuário pode conter apenas letras, números e os seguintes caracteres especiais: -._@+");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha é obrigatória")
            .Must(PasswordRegex.IsMatch)
            .WithMessage("Senha precisa conter no mínimo 6 caracteres, contendo pelo menos: 1 número, 1 letra maiúscula, 1 letra minúscula e 1 caracter especial");
    }
}
