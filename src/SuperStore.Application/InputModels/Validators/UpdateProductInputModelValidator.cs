using FluentValidation;

namespace SuperStore.Application.InputModels.Validators;

public sealed class UpdateProductInputModelValidator : AbstractValidator<UpdateProductInputModel>
{
    public UpdateProductInputModelValidator()
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

        RuleFor(x => x.Category)
            .NotEmpty();

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1);
    }
}