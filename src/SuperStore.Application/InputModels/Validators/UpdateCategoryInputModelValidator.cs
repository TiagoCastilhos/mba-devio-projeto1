using FluentValidation;

namespace SuperStore.Application.InputModels.Validators;

public sealed class UpdateCategoryInputModelValidator : AbstractValidator<UpdateCategoryInputModel>
{
    public UpdateCategoryInputModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1);
    }
}