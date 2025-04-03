using Microsoft.EntityFrameworkCore;
using SuperStore.Data.Abstractions.Contexts;
using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Model.Entities;

namespace SuperStore.Data.Repositories;
internal sealed class CategoriesRepository : RepositoryBase<Category>, ICategoriesRepository
{
    public CategoriesRepository(ISuperStoreDbContext context)
        : base(context, context.Categories)
    {
    }

    public async Task<IReadOnlyCollection<Category>> GetByUserAsync(string userId, CancellationToken cancellationToken)
    {
        return await DbSet.Where(c => c.CreatedBy.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await DbSet.FirstOrDefaultAsync(c => EF.Functions.Like(c.Name, name), cancellationToken);
    }
}
