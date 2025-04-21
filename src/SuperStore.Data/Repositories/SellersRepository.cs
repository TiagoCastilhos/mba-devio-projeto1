using Microsoft.EntityFrameworkCore;
using SuperStore.Data.Abstractions.Contexts;
using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Data.Entities;

namespace SuperStore.Data.Repositories;
internal sealed class SellersRepository : RepositoryBase<Seller>, ISellersRepository
{
    public SellersRepository(ISuperStoreDbContext context)
        : base(context, context.Sellers)
    {
    }

    public async Task<Seller?> GetAsync(string userId, CancellationToken cancellationToken)
    {
        return await DbSet.FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);
    }
}
