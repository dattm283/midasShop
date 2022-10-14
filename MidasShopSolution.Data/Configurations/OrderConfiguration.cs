using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MidasShopSolution.Data.Entites;

namespace MidasShopSolution.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(t => t.Id);
        builder
            .Property(t => t.Id)
            .UseIdentityColumn();
        builder
            .Property(t => t.OrderDate)
            .HasDefaultValue(DateTime.Now);
        builder
            .Property(t => t.ShipEmail)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(50);
        builder
            .Property(t => t.ShipAddress)
            .IsRequired().HasMaxLength(200);
        builder
            .Property(t => t.ShipName)
            .IsRequired()
            .HasMaxLength(200);
        builder
            .Property(t => t.ShipPhoneNumber)
            .IsRequired()
            .HasMaxLength(200);
        builder
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);
    }
}