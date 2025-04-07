using SuperStore.Application.InputModels;
using SuperStore.Application.OutputModels;

namespace SuperStore.Application.Abstractions.Services;
public interface IProductsService
{
    Task<ProductOutputModel?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<ProductOutputModel>> ShowcaseAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<ProductOutputModel>> GetAsync(CancellationToken cancellationToken);
    Task<ProductOutputModel> CreateAsync(CreateProductInputModel inputModel, CancellationToken cancellationToken);
    Task<ProductOutputModel> UpdateAsync(UpdateProductInputModel inputModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
