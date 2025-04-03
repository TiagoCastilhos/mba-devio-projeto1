using FluentValidation;
using SuperStore.Data.Abstractions.Repositories;

namespace SuperStore.Application.InputModels.Validators;

public sealed class CreateCategoryInputModelValidator : AbstractValidator<CreateCategoryInputModel>
{
    public CreateCategoryInputModelValidator(ICategoriesRepository categoriesRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30)
            .MustAsync(async (name, ct) =>
            {
                var existingCategory = await categoriesRepository.GetByNameAsync(name, ct);
                return existingCategory == null;
            })
            .WithMessage("Já existe uma categoria com este nome");
    }
}
