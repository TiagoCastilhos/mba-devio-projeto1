using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Model.Entities;
using SupperStore.Application.Abstractions.Services;
using SupperStore.Application.InputModels;
using SupperStore.Application.OutputModels;

namespace SupperStore.Application.Services;

internal sealed class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;
    private readonly ISellersRepository _sellersRepository;
    private readonly ICategoriesRepository _categoriesRepository;

    public ProductsService(
        IProductsRepository productsRepository,
        ISellersRepository sellersRepository,
        ICategoriesRepository categoriesRepository)
    {
        _productsRepository = productsRepository;
        _sellersRepository = sellersRepository;
        _categoriesRepository = categoriesRepository;
    }

    public async Task<IReadOnlyCollection<ProductOutputModel>> GetAsync(CancellationToken cancellationToken)
    {
        var products = await _productsRepository.GetAsync(cancellationToken);
        return [.. products.Select(product => new ProductOutputModel(product))];
    }

    public async Task<ProductOutputModel> CreateAsync(CreateProductInputModel inputModel, CancellationToken cancellationToken)
    {
        var seller = await _sellersRepository.GetAsync(1, cancellationToken); //Get user id from request
        var category = await _categoriesRepository.GetAsync(inputModel.CategoryId, cancellationToken);

        var product = new Product(inputModel.Name, inputModel.Description, inputModel.Price, inputModel.Quantity, seller, category);

        await _productsRepository.AddAsync(product, cancellationToken);
        await _productsRepository.SaveChangesAsync(cancellationToken);

        return new ProductOutputModel(product);
    }

    public async Task<ProductOutputModel> UpdateAsync(UpdateProductInputModel inputModel, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetAsync(inputModel.Id, cancellationToken);
        if (product == null)
        {
            //throw exception
        }

        var category = await _categoriesRepository.GetAsync(inputModel.CategoryId, cancellationToken); //not needed if category is not updated
        if (category == null)
        {
            //throw exception
        }

        product.ChangeName(inputModel.Name);
        product.ChangeDescription(inputModel.Description);
        product.ChangePrice(inputModel.Price);
        product.ChangeQuantity(inputModel.Quantity);
        product.ChangeCategory(category);

        await _productsRepository.SaveChangesAsync(cancellationToken);

        return new ProductOutputModel(product);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetAsync(id, cancellationToken);

        if (product == null)
        {
            //throw exception
        }

        _productsRepository.Delete(product);
        await _productsRepository.SaveChangesAsync(cancellationToken);
    }
}