using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MidasShopSolution.Data.Entites;

namespace MidasShopSolution.Data.Configurations;

public class CategoryTranslationConfiguration : IEntityTypeConfiguration<CategoryTranslation>
{
    public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
    {
        builder.ToTable("CategoryTranslations");

        builder.HasKey(ct => ct.Id);

        builder
            .Property(ct => ct.Id)
            .UseIdentityColumn();
        builder
            .Property(ct => ct.Name)
            .IsRequired()
            .HasMaxLength(200);
        builder
            .Property(ct => ct.SeoAlias)
            .IsRequired()
            .HasMaxLength(200);
        builder
            .Property(ct => ct.SeoDescription)
            .HasMaxLength(500);
        builder
            .Property(ct => ct.SeoTitle)
            .HasMaxLength(200);
        builder
            .Property(ct => ct.LanguageId)
            .IsUnicode(false)
            .IsRequired()
            .HasMaxLength(5);

        builder
            .HasOne(ct => ct.Language)
            .WithMany(l => l.CategoryTranslations)
            .HasForeignKey(ct => ct.LanguageId);

        builder
            .HasOne(ct => ct.Category)
            .WithMany(c => c.CategoryTranslations)
            .HasForeignKey(ct => ct.CategoryId);
    }
}