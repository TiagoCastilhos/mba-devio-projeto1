using SuperStore.Model.Entities;

namespace SuperStore.Data.Abstractions.Repositories;

public interface IRepository<T> where T : EntityBase
{
    Task<IReadOnlyCollection<T>> GetAsync(CancellationToken cancellationToken);
    Task<T?> GetAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    void Delete(T entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
