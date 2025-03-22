using Microsoft.EntityFrameworkCore;
using SuperStore.Model.Entities;

namespace SuperStore.Data.Abstractions.Contexts;
public interface ISuperStoreDbContext
{
    DbSet<Category> Categories { get; }
    DbSet<Product> Products { get; }
    DbSet<Seller> Sellers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
