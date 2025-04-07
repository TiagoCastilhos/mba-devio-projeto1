using Microsoft.EntityFrameworkCore;
using SuperStore.Data.Abstractions.Contexts;
using SuperStore.Model.Entities;

namespace SuperStore.Data.Repositories;

internal abstract class RepositoryBase<T> where T : EntityBase
{
    protected ISuperStoreDbContext Context { get; }
    protected DbSet<T> DbSet { get; }

    protected RepositoryBase(ISuperStoreDbContext context, DbSet<T> dbSet)
    {
        Context = context;
        DbSet = dbSet;
    }

    public async Task<IReadOnlyCollection<T>> GetAsync(CancellationToken cancellationToken)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public async Task<T?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public void Delete(T entity)
    {
        DbSet.Remove(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await Context.SaveChangesAsync(cancellationToken);
    }
}