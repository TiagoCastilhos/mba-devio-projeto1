using FluentValidation;

namespace SuperStore.Application.InputModels.Validators;

public sealed class CreateCategoryInputModelValidator : AbstractValidator<CreateCategoryInputModel>
{
    public CreateCategoryInputModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30);
    }
}
