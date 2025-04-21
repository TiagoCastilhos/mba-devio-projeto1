using Microsoft.EntityFrameworkCore;
using SuperStore.Data.Abstractions.Contexts;
using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Data.Entities;

namespace SuperStore.Data.Repositories;
internal sealed class ProductsRepository : RepositoryBase<Product>, IProductsRepository
{
    public ProductsRepository(ISuperStoreDbContext context)
        : base(context, context.Products)
    {
    }

    public async Task<IReadOnlyCollection<Product>> GetAsync(string userId, CancellationToken cancellationToken)
    {
        return await DbSet.Where(c => c.CreatedBy.UserId == userId).ToListAsync(cancellationToken);
    }
}
