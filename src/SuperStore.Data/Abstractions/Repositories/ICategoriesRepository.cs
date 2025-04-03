using SuperStore.Model.Entities;

namespace SuperStore.Data.Abstractions.Repositories;

public interface ICategoriesRepository : IRepository<Category>
{
    Task<IReadOnlyCollection<Category>> GetByUserAsync(string userId, CancellationToken cancellationToken);
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
