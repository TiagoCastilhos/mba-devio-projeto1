using FluentValidation;

namespace SuperStore.Core.InputModels.Validators;

public sealed class CreateProductInputModelValidator : AbstractValidator<CreateProductInputModel>
{
    public CreateProductInputModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(50)
            .WithMessage("O nome do produto deve ter no máximo 50 caracteres.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("A descrição do produto é obrigatória.")
            .MaximumLength(200)
            .WithMessage("A descrição do produto deve ter no máximo 200 caracteres.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O preço do produto deve ser maior ou igual a 0.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("A quantidade do produto deve ser maior ou igual a 0.");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("A categoria do produto é obrigatória.");
    }
}
