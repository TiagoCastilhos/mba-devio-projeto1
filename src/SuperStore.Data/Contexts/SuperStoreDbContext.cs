using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuperStore.Data.Abstractions.Contexts;
using SuperStore.Data.Entities;

namespace SuperStore.Data.Contexts;
public sealed class SuperStoreDbContext : IdentityDbContext, ISuperStoreDbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Seller> Sellers { get; set; }

    public SuperStoreDbContext(DbContextOptions options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            ConfigureValueConverters(entityType);
        }
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

    private static void ConfigureValueConverters(IMutableEntityType entityType)
    {
        foreach (var property in entityType.GetProperties())
        {
            if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
            {
                property.SetValueConverter(new ValueConverter<DateTime, string>(
                    d => d.ToString("yyyy-MM-dd HH:mm:ss"),
                    s => DateTime.Parse(s)));
            }
            else if (property.ClrType == typeof(bool) || property.ClrType == typeof(bool?))
            {
                property.SetValueConverter(new ValueConverter<bool, int>(
                    b => b ? 1 : 0,
                    i => i == 1));
            }
            else if (property.ClrType == typeof(Guid) || property.ClrType == typeof(Guid?))
            {
                property.SetValueConverter(new ValueConverter<Guid, string>(
                    g => g.ToString(),
                    s => Guid.Parse(s)));
            }
        }
    }
}