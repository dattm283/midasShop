using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MidasShopSolution.Data.Entites;

namespace MidasShopSolution.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Price).IsRequired();
        builder.Property(t => t.OriginalPrice).IsRequired();
        builder.Property(t => t.Stock).IsRequired()
            .HasDefaultValue(0);
        builder.Property(t => t.ViewCount).IsRequired()
            .HasDefaultValue(0);
    }
}