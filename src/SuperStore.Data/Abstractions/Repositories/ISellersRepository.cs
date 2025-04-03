using SuperStore.Model.Entities;

namespace SuperStore.Data.Abstractions.Repositories;

public interface ISellersRepository : IRepository<Seller>
{
    Task<Seller?> GetAsync(string userId, CancellationToken cancellationToken);
}