using Microsoft.AspNetCore.Http;
using SuperStore.Application.Abstractions.Services;
using SuperStore.Application.Exceptions;
using SuperStore.Application.InputModels;
using SuperStore.Application.OutputModels;
using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Model.Entities;

namespace SuperStore.Application.Services;
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

    public async Task<IReadOnlyCollection<ProductOutputModel>> GetAsync(CancellationToken cancellationToken)
    {
        var userId = GetUserId()!;
        var products = await _productsRepository.GetAsync(userId, cancellationToken);
        return [.. products.Select(product => new ProductOutputModel(product))];
    }

    public async Task<ProductOutputModel?> GetAsync(int id, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetAsync(id, cancellationToken);
        
        if (product == null)
            return null;
        
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

        if (product.Category.Name != inputModel.Category)
        {
            var category = await _categoriesRepository.GetByNameAsync(inputModel.Category, cancellationToken)
                ?? throw new EntityNotFoundException(nameof(Category), inputModel.Category);

            product.ChangeCategory(category);
        }

        product.ChangeName(inputModel.Name);
        product.ChangeDescription(inputModel.Description);
        product.ChangePrice(inputModel.Price);
        product.ChangeQuantity(inputModel.Quantity);

        if (!string.IsNullOrEmpty(inputModel.ImageUrl))
            product.ChangeImage(inputModel.ImageUrl);

        await _productsRepository.SaveChangesAsync(cancellationToken);

        return new ProductOutputModel(product);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(Product), id);

        _productsRepository.Delete(product);
        await _productsRepository.SaveChangesAsync(cancellationToken);
    }
}