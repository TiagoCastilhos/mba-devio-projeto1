using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperStore.Data.Entities;

namespace SuperStore.Data.Contexts.Configuration;

internal abstract class ConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
{
    public abstract string TableName { get; }

    public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);

    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(TableName);
        builder.HasKey(e => e.Id);

        ConfigureEntity(builder);

        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}