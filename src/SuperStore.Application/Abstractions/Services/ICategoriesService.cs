using SuperStore.Application.InputModels;
using SuperStore.Application.OutputModels;

namespace SuperStore.Application.Abstractions.Services;
public interface ICategoriesService
{
    Task<CategoryOutputModel?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<CategoryOutputModel>> GetAsync(CancellationToken cancellationToken);
    Task<CategoryOutputModel> CreateAsync(CreateCategoryInputModel inputModel, CancellationToken cancellationToken);
    Task<CategoryOutputModel> UpdateAsync(UpdateCategoryInputModel inputModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}