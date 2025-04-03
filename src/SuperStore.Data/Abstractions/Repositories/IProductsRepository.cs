using SuperStore.Model.Entities;

namespace SuperStore.Data.Abstractions.Repositories;

public interface IProductsRepository : IRepository<Product>
{
    Task<IReadOnlyCollection<Product>> GetAsync(string userId, CancellationToken cancellationToken);
}
