using Microsoft.EntityFrameworkCore;
using SuperStore.Data.Abstractions.Contexts;
using SuperStore.Model.Entities;

namespace SuperStore.Data.Repositories;

internal abstract class RepositoryBase<T> where T : EntityBase
{
    private readonly ISuperStoreDbContext _context;
    private readonly DbSet<T> _dbSet;

    protected RepositoryBase(ISuperStoreDbContext context, DbSet<T> dbSet)
    {
        _context = context;
        _dbSet = dbSet;
    }

    public async Task<IReadOnlyCollection<T>> GetAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<T?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}