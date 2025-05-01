using Microsoft.AspNetCore.Http;
using SuperStore.Core.Abstractions.Services;
using SuperStore.Core.Exceptions;
using SuperStore.Core.InputModels;
using SuperStore.Core.OutputModels;
using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Data.Entities;

namespace SuperStore.Core.Services;
internal sealed class CategoriesService : ServiceBase, ICategoriesService
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly ISellersRepository _sellersRepository;

    public CategoriesService(
        ICategoriesRepository categoriesRepository,
        ISellersRepository sellersRepository,
        IHttpContextAccessor httpContextAccessor)
        : base(httpContextAccessor)
    {
        _categoriesRepository = categoriesRepository;
        _sellersRepository = sellersRepository;
    }

    public async Task<IReadOnlyCollection<CategoryOutputModel>> GetAsync(CancellationToken cancellationToken)
    {
        var categories = await _categoriesRepository.GetAsync(cancellationToken);
        return [.. categories.Select(category => new CategoryOutputModel(category))];
    }

    public async Task<CategoryOutputModel?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Category), id);

        return new CategoryOutputModel(category);
    }

    public async Task<CategoryOutputModel> CreateAsync(CreateCategoryInputModel inputModel, CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        var seller = await _sellersRepository.GetAsync(userId!, cancellationToken);

        var category = new Category(inputModel.Name, seller);
        await _categoriesRepository.AddAsync(category, cancellationToken);
        await _categoriesRepository.SaveChangesAsync(cancellationToken);

        return new CategoryOutputModel(category);
    }

    public async Task<CategoryOutputModel> UpdateAsync(UpdateCategoryInputModel inputModel, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetAsync(inputModel.Id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Category), inputModel.Id);

        category.Name = inputModel.Name;
        category.UpdatedOn = DateTime.UtcNow;

        await _categoriesRepository.SaveChangesAsync(cancellationToken);

        return new CategoryOutputModel(category);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        var category = await _categoriesRepository.GetAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Category), id);

        if (category.Products.Count > 0)
            throw new EntityHasRelatedEntitiesException(nameof(Category), id);

        category.UpdatedOn = DateTime.UtcNow;

        _categoriesRepository.Delete(category);
        await _categoriesRepository.SaveChangesAsync(cancellationToken);
    }
}
