using FluentValidation;

namespace SuperStore.Core.InputModels.Validators;

public sealed class UserSignInInputModelValidator : AbstractValidator<UserSignInInputModel>
{
    public UserSignInInputModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email é obrigatório")
            .EmailAddress()
            .WithMessage("Email inválido");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha é obrigatória")
            .Must(CreateUserInputModelValidator.PasswordRegex.IsMatch)
            .WithMessage("Senha precisa conter no mínimo 6 caracteres, contendo pelo menos: 1 número, 1 letra maiúscula, 1 letra minúscula e 1 caracter especial");
    }
}