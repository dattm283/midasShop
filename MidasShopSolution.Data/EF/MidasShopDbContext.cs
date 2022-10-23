using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MidasShopSolution.Data.Configurations;
using MidasShopSolution.Data.Entites;

namespace MidasShopSolution.Data.EF;

public class MidasShopDbContext : IdentityDbContext<User, Role, Guid>
{
    public MidasShopDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure using fluent API
        modelBuilder.ApplyConfiguration(new CartConfiguration());

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());

        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());

        modelBuilder.ApplyConfiguration(new ContactConfiguration());

        modelBuilder.ApplyConfiguration(new LanguageConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new ProductTranslationConfiguration());

        modelBuilder.ApplyConfiguration(new PromotionConfiguration());


        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new ProductImageConfiguration());

        modelBuilder
            .Entity<IdentityUserClaim<Guid>>()
            .ToTable("UserClaims");
        modelBuilder
            .Entity<IdentityUserRole<Guid>>()
            .ToTable("UserRoles")
            .HasKey(iur => new { iur.RoleId, iur.UserId });
        modelBuilder.Entity<IdentityUserLogin<Guid>>()
            .ToTable("UserLogins")
            .HasKey(iul => iul.UserId);

        modelBuilder
            .Entity<IdentityRoleClaim<Guid>>()
            .ToTable("RoleClaims");
        modelBuilder
            .Entity<IdentityUserToken<Guid>>()
            .ToTable("UserTokens")
            .HasKey(iut => iut.UserId);

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DbSet<Cart> Carts { get; set; }

    public DbSet<CategoryTranslation> CategoryTranslations { get; set; }

    public DbSet<Contact> Contacts { get; set; }

    public DbSet<Language> Languages { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<ProductTranslation> ProductTranslations { get; set; }

    public DbSet<Promotion> Promotions { get; set; }


    public DbSet<ProductImage> ProductImages { get; set; }
}