using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperStore.Data.Entities;

namespace SuperStore.Data.Contexts.Configuration;

internal sealed class CategoryConfiguration : ConfigurationBase<Category>
{
    public override string TableName => "Categories";

    public override void ConfigureEntity(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasOne(c => c.CreatedBy)
            .WithMany(sp => sp.Categories)
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.Name)
            .IsUnique(false);
    }
}
