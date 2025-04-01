using FluentValidation;

namespace SuperStore.Authorization.InputModels.Validators;

public sealed class UserSignInInputModelValidator : AbstractValidator<UserSignInInputModel>
{
    public UserSignInInputModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Must(CreateUserInputModelValidator.PasswordRegex.IsMatch)
            .WithMessage("Senha precisa conter no mínimo 6 caracteres, 1 número, 1 letra maiúscula, 1 letra minúscula e 1 caracter especial");
    }
}