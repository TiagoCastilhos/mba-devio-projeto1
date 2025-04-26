using FluentValidation;
using SuperStore.Data.Abstractions.Repositories;

namespace SuperStore.Core.InputModels.Validators;

public sealed class CreateCategoryInputModelValidator : AbstractValidator<CreateCategoryInputModel>
{
    public CreateCategoryInputModelValidator(ICategoriesRepository categoriesRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("O nome da categoria é obrigatório")
            .MaximumLength(30)
            .WithMessage("O nome da categoria deve ter no máximo 30 caracteres")
            .MustAsync(async (name, ct) =>
            {
                var existingCategory = await categoriesRepository.GetByNameAsync(name, ct);
                return existingCategory == null;
            })
            .WithMessage("Já existe uma categoria com este nome");
    }
}
