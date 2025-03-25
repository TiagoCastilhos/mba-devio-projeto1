using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperStore.Model.Entities;

namespace SuperStore.Data.Contexts.Configuration;

internal sealed class SellerConfiguration : ConfigurationBase<Seller>
{
    public override string TableName => "Sellers";

    public override void ConfigureEntity(EntityTypeBuilder<Seller> builder)
    {
        builder.Property(sp => sp.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(sp => sp.UserId)
            .HasMaxLength(40)
            .IsRequired();
    }
}