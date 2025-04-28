using SuperStore.Data.Entities;

namespace SuperStore.Data.Abstractions.Repositories;

public interface IProductsRepository : IRepository<Product>
{
    Task<IReadOnlyCollection<Product>> GetAsync(string userId, string? categoryName = null, CancellationToken cancellationToken = default);
}
