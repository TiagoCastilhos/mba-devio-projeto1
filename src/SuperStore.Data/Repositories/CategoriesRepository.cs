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
}
