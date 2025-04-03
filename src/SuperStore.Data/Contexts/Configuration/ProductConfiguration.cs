using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperStore.Model.Entities;

namespace SuperStore.Data.Contexts.Configuration;
internal sealed class ProductConfiguration : ConfigurationBase<Product>
{
    public override string TableName => "Products";

    public override void ConfigureEntity(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Quantity)
            .IsRequired();

        builder.Property(p => p.ImageUrl)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.HasOne(p => p.CreatedBy)
            .WithMany(sp => sp.Products)
            .HasForeignKey(p => p.CreatedById);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
    }
}
