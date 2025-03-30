using FluentValidation;

namespace SuperStore.Application.InputModels.Validators;

public sealed class CreateProductInputModelValidator : AbstractValidator<CreateProductInputModel>
{
    public CreateProductInputModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.CategoryId)
            .GreaterThanOrEqualTo(1);
    }
}
