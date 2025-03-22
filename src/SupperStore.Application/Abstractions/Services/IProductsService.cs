using SupperStore.Application.InputModels;
using SupperStore.Application.OutputModels;

namespace SupperStore.Application.Abstractions.Services;
public interface IProductsService
{
    Task<IReadOnlyCollection<ProductOutputModel>> GetAsync(CancellationToken cancellationToken);
    Task<ProductOutputModel> CreateAsync(CreateProductInputModel inputModel, CancellationToken cancellationToken);
    Task<ProductOutputModel> UpdateAsync(UpdateProductInputModel inputModel, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}
