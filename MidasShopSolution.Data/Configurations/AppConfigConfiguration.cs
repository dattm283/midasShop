using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MidasShopSolution.Data.Entites;

namespace MidasShopSolution.Data.Configurations;

public class AppConfigConfiguration : IEntityTypeConfiguration<AppConfig>
{
    public void Configure(EntityTypeBuilder<AppConfig> builder)
    {
        builder.ToTable("AppConfigs");
        builder.HasKey(t => t.Key);
        builder.Property(t => t.Value)
            .IsRequired(true);
    }
}