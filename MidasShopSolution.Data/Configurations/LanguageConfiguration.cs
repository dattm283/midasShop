using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MidasShopSolution.Data.Entites;

namespace MidasShopSolution.Data.Configurations;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("Languages");
        
        builder.HasKey(l => l.Id);
        
        builder
            .Property(l => l.Id)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(5);
        builder
            .Property(l => l.Name)
            .IsRequired()
            .HasMaxLength(20);
    }
}