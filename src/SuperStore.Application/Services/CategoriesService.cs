﻿using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Model.Entities;
using SuperStore.Application.Abstractions.Services;
using SuperStore.Application.InputModels;
using SuperStore.Application.OutputModels;

namespace SuperStore.Application.Services;

internal sealed class CategoriesService : ICategoriesService
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly ISellersRepository _sellersRepository;

    public CategoriesService(ICategoriesRepository categoriesRepository, ISellersRepository sellersRepository)
    {
        _categoriesRepository = categoriesRepository;
        _sellersRepository = sellersRepository;
    }

    public async Task<IReadOnlyCollection<CategoryOutputModel>> GetAsync(CancellationToken cancellationToken)
    {
        var categories = await _categoriesRepository.GetAsync(cancellationToken);
        return [.. categories.Select(category => new CategoryOutputModel(category))];
    }

    public async Task<CategoryOutputModel> CreateAsync(CreateCategoryInputModel inputModel, CancellationToken cancellationToken)
    {
        var seller = await _sellersRepository.GetAsync(1, cancellationToken); //Get user id from request

        var category = new Category(inputModel.Name, seller);
        await _categoriesRepository.AddAsync(category, cancellationToken);
        await _categoriesRepository.SaveChangesAsync(cancellationToken);

        return new CategoryOutputModel(category);
    }

    public async Task<CategoryOutputModel> UpdateAsync(UpdateCategoryInputModel inputModel, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetAsync(inputModel.Id, cancellationToken);

        if (category == null)
        {
            //throw exception
        }

        category.ChangeName(inputModel.Name);

        await _categoriesRepository.SaveChangesAsync(cancellationToken);

        return new CategoryOutputModel(category);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetAsync(id, cancellationToken);

        if (category == null)
        {
            //throw exception
        }

        _categoriesRepository.Delete(category);
        await _categoriesRepository.SaveChangesAsync(cancellationToken);
    }
}
