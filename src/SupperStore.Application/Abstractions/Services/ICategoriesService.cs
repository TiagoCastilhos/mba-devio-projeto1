using SupperStore.Application.InputModels;
using SupperStore.Application.OutputModels;

namespace SupperStore.Application.Abstractions.Services;
public interface ICategoriesService
{
    Task<IReadOnlyCollection<CategoryOutputModel>> GetAsync(CancellationToken cancellationToken);
    Task<CategoryOutputModel> CreateAsync(CreateCategoryInputModel inputModel, CancellationToken cancellationToken);
    Task<CategoryOutputModel> UpdateAsync(UpdateCategoryInputModel inputModel, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}