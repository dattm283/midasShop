using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MidasShopSolution.Data.Entites;
using MidasShopSolution.Data.Enums;

namespace MidasShopSolution.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).UseIdentityColumn();
        builder.Property(t => t.Status).HasDefaultValue(Status.Active);

        builder
           .Property(c => c.Name)
           .IsRequired()
           .HasMaxLength(200);
        builder
            .Property(c => c.SeoAlias)
            .IsRequired()
            .HasMaxLength(200);
        builder
            .Property(c => c.SeoDescription)
            .HasMaxLength(500);
        builder
            .Property(c => c.SeoTitle)
            .HasMaxLength(200);
        // builder.HasOne(c => c.Product).WithMany(p => p.Categories);
    }
}