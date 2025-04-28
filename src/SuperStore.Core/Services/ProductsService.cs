using Microsoft.AspNetCore.Http;
using SuperStore.Core.Abstractions.Services;
using SuperStore.Core.Exceptions;
using SuperStore.Core.InputModels;
using SuperStore.Core.OutputModels;
using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Data.Entities;

namespace SuperStore.Core.Services;
internal sealed class ProductsService : ServiceBase, IProductsService
{
    private readonly IProductsRepository _productsRepository;
    private readonly ISellersRepository _sellersRepository;
    private readonly ICategoriesRepository _categoriesRepository;

    public ProductsService(
        IProductsRepository productsRepository,
        ISellersRepository sellersRepository,
        ICategoriesRepository categoriesRepository,
        IHttpContextAccessor httpContextAccessor)
        : base(httpContextAccessor)
    {
        _productsRepository = productsRepository;
        _sellersRepository = sellersRepository;
        _categoriesRepository = categoriesRepository;
    }

    public async Task<IReadOnlyCollection<ProductOutputModel>> ShowcaseAsync(CancellationToken cancellationToken)
    {
        var products = await _productsRepository.GetAsync(cancellationToken);
        return [.. products.Select(product => new ProductOutputModel(product))];
    }

    public async Task<IReadOnlyCollection<ProductOutputModel>> GetAsync(string? categoryName, CancellationToken cancellationToken)
    {
        var userId = GetUserId()!;
        var products = await _productsRepository.GetAsync(userId, categoryName, cancellationToken);
        return [.. products.Select(product => new ProductOutputModel(product))];
    }

    public async Task<ProductOutputModel?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = GetUserId()!;

        var product = await _productsRepository.GetAsync(id, cancellationToken);

        if (product == null || product.CreatedBy.UserId != userId)
            throw new EntityNotFoundException(nameof(Product), id);

        return new ProductOutputModel(product);
    }

    public async Task<ProductOutputModel> CreateAsync(CreateProductInputModel inputModel, CancellationToken cancellationToken)
    {
        var userId = GetUserId()!;

        var seller = await _sellersRepository.GetAsync(userId, cancellationToken);

        var category = await _categoriesRepository.GetByNameAsync(inputModel.Category, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Category), inputModel.Category);

        var product = new Product(inputModel.Name, inputModel.Description, inputModel.Price,
            inputModel.Quantity, inputModel.ImageUrl, seller, category);

        await _productsRepository.AddAsync(product, cancellationToken);
        await _productsRepository.SaveChangesAsync(cancellationToken);

        return new ProductOutputModel(product);
    }

    public async Task<ProductOutputModel> UpdateAsync(UpdateProductInputModel inputModel, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetAsync(inputModel.Id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Product), inputModel.Id);

        var userId = GetUserId()!;

        if (product.CreatedBy.UserId != userId)
            throw new EntityNotFoundException(nameof(Product), inputModel.Id);

        if (product.Category.Name != inputModel.Category)
        {
            var category = await _categoriesRepository.GetByNameAsync(inputModel.Category, cancellationToken)
                ?? throw new EntityNotFoundException(nameof(Category), inputModel.Category);

            product.Category = category;
        }

        product.Name = inputModel.Name;
        product.Description = inputModel.Description;
        product.Price = inputModel.Price;
        product.Quantity = inputModel.Quantity;

        if (!string.IsNullOrEmpty(inputModel.ImageUrl))
            product.ImageUrl = inputModel.ImageUrl;

        await _productsRepository.SaveChangesAsync(cancellationToken);

        return new ProductOutputModel(product);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Product), id);

        var userId = GetUserId()!;

        if (product.CreatedBy.UserId != userId)
            throw new EntityNotFoundException(nameof(Product), id);

        _productsRepository.Delete(product);
        await _productsRepository.SaveChangesAsync(cancellationToken);
    }
}