using SuperStore.Data.Abstractions.Contexts;
using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Model.Entities;

namespace SuperStore.Data.Repositories;
internal sealed class SellersRepository : RepositoryBase<Seller>, ISellersRepository
{
    public SellersRepository(ISuperStoreDbContext context)
        : base(context, context.Sellers)
    {
    }
}
