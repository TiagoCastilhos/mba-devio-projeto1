using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
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

        builder.Property(e => e.CreatedOn)
            .HasValueGenerator<DateTimeValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.UpdatedOn)
            .HasValueGenerator<DateTimeValueGenerator>()
            //.ValueGeneratedOnAddOrUpdate(); //TODO: this is not working, don't know why yet
            .ValueGeneratedOnAdd();

        builder.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}

internal class DateTimeValueGenerator : ValueGenerator<DateTime>
{
    public override bool GeneratesTemporaryValues => false;

    public override DateTime Next(EntityEntry entry)
    {
        return DateTime.UtcNow;
    }
}