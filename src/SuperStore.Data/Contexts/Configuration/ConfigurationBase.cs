using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperStore.Model.Entities;

namespace SuperStore.Data.Contexts.Configuration;

internal abstract class ConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
{
    public abstract string TableName { get; }

    public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);

    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(TableName);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        ConfigureEntity(builder);

        builder.Property(e => e.CreatedOn)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.UpdatedOn)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAddOrUpdate();

        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
