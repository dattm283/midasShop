using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MidasShopSolution.Data.Entites;

namespace MidasShopSolution.Data.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");
        builder.HasKey(c => c.Id);
        builder
            .Property(c => c.Id)
            .UseIdentityColumn();
        builder
            .Property(c => c.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(c => c.Email)
            .HasMaxLength(200)
            .IsRequired();
        builder
            .Property(c => c.PhoneNumber)
            .HasMaxLength(200)
            .IsRequired();
        builder
            .Property(c => c.Message)
            .IsRequired();
    }
}