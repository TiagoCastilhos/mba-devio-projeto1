using Microsoft.AspNetCore.Identity;
using SuperStore.Application.Abstractions.Services;
using SuperStore.Application.Exceptions;
using SuperStore.Application.InputModels;
using SuperStore.Application.OutputModels;
using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Model.Entities;

namespace SuperStore.Application.Services;

internal sealed class CategoriesService : ICategoriesService
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly ISellersRepository _sellersRepository;
    private readonly SignInManager<IdentityUser> _signInManager;

    public CategoriesService(
        ICategoriesRepository categoriesRepository, 
        ISellersRepository sellersRepository, 
        SignInManager<IdentityUser> signInManager)
    {
        _categoriesRepository = categoriesRepository;
        _sellersRepository = sellersRepository;
        _signInManager = signInManager;
    }

    public async Task<IReadOnlyCollection<CategoryOutputModel>> GetAsync(CancellationToken cancellationToken)
    {
        var categories = await _categoriesRepository.GetAsync(cancellationToken);
        return [.. categories.Select(category => new CategoryOutputModel(category))];
    }

    public async Task<CategoryOutputModel> CreateAsync(CreateCategoryInputModel inputModel, CancellationToken cancellationToken)
    {
        var a = _signInManager.Context.User;
        var seller = await _sellersRepository.GetAsync(1, cancellationToken); //Get user id from request

        var category = new Category(inputModel.Name, seller);
        await _categoriesRepository.AddAsync(category, cancellationToken);
        await _categoriesRepository.SaveChangesAsync(cancellationToken);

        return new CategoryOutputModel(category);
    }

    public async Task<CategoryOutputModel> UpdateAsync(UpdateCategoryInputModel inputModel, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetAsync(inputModel.Id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Category), inputModel.Id);

        category.ChangeName(inputModel.Name);

        await _categoriesRepository.SaveChangesAsync(cancellationToken);

        return new CategoryOutputModel(category);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Category), id);

        _categoriesRepository.Delete(category);
        await _categoriesRepository.SaveChangesAsync(cancellationToken);
    }
}
