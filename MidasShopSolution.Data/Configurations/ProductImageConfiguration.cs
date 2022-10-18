using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MidasShopSolution.Data.Entites;

namespace MidasShopSolution.Data.Configurations;

public class ProductImageConfiguration: IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImages");
        builder.HasKey(pi => pi.Id);
        builder.Property(pi => pi.Id).UseIdentityColumn();
        builder.Property(pi => pi.ImagePath).HasMaxLength(256).IsRequired(true);
        builder.Property(pi => pi.Caption).HasMaxLength(256).IsRequired(true);

        builder
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(pi => pi.ProductId);
    }
}