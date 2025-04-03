using FluentValidation;
using SuperStore.Data.Abstractions.Repositories;

namespace SuperStore.Application.InputModels.Validators;

public sealed class UpdateCategoryInputModelValidator : AbstractValidator<UpdateCategoryInputModel>
{
    public UpdateCategoryInputModelValidator(ICategoriesRepository categoriesRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30)
            .MustAsync(async (inputModel, name, ct) =>
            {
                var existingCategory = await categoriesRepository.GetByNameAsync(name, ct);

                return existingCategory == null || existingCategory.Id == inputModel.Id;
            })
            .WithMessage("Já existe uma categoria com este nome"); ;

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1);
    }
}