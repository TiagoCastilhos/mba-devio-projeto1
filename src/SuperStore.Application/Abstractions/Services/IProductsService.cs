using SuperStore.Application.InputModels;
using SuperStore.Application.OutputModels;

namespace SuperStore.Application.Abstractions.Services;
public interface IProductsService
{
    Task<IReadOnlyCollection<ProductOutputModel>> GetAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<ProductOutputModel>> GetAsync(string userId, CancellationToken cancellationToken);
    Task<ProductOutputModel> CreateAsync(CreateProductInputModel inputModel, CancellationToken cancellationToken);
    Task<ProductOutputModel> UpdateAsync(UpdateProductInputModel inputModel, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}
