using SuperStore.Data.Abstractions.Contexts;
using SuperStore.Data.Abstractions.Repositories;
using SuperStore.Model.Entities;

namespace SuperStore.Data.Repositories;
internal sealed class ProductsRepository : RepositoryBase<Product>, IProductsRepository
{
    public ProductsRepository(ISuperStoreDbContext context)
        : base(context, context.Products)
    {
    }
}
