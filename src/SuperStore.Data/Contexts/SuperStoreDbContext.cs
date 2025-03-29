using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuperStore.Data.Abstractions.Contexts;
using SuperStore.Model.Entities;
using System.Reflection;

namespace SuperStore.Data.Contexts;
internal sealed class SuperStoreDbContext : DbContext, ISuperStoreDbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<IdentityUser> Users { get; set; }

    public SuperStoreDbContext(DbContextOptions options) 
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplySoftDelete();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplySoftDelete()
    {
        var deletedEntities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Deleted && e.Entity is EntityBase);

        foreach (var entityEntry in deletedEntities)
        {
            entityEntry.State = EntityState.Modified;
            entityEntry.CurrentValues[nameof(EntityBase.IsDeleted)] = true;
        }
    }
}
