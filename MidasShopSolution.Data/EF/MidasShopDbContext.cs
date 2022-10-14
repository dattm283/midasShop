using Microsoft.EntityFrameworkCore;
using MidasShopSolution.Data.Configurations;
using MidasShopSolution.Data.Entites;
using MidasShopSolution.Data.Extensions;

namespace MidasShopSolution.Data.EF;

public class MidasShopDbContext : DbContext
{
    public MidasShopDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure unsing fluent API
        modelBuilder.ApplyConfiguration(new CartConfiguration());

        modelBuilder.ApplyConfiguration(new AppConfigConfiguration());

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductInCategoryConfiguration());

        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());

        modelBuilder.ApplyConfiguration(new ContactConfiguration());

        modelBuilder.ApplyConfiguration(new LanguageConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryTranslationConfiguration());
        modelBuilder.ApplyConfiguration(new ProductTranslationConfiguration());

        modelBuilder.ApplyConfiguration(new PromotionConfiguration());

        modelBuilder.ApplyConfiguration(new TransactionConfiguration());

        // Data seeding
        modelBuilder.Seed();
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DbSet<AppConfig> AppConfigs { get; set; }


    public DbSet<Cart> Carts { get; set; }

    public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
    public DbSet<ProductInCategory> ProductInCategories { get; set; }

    public DbSet<Contact> Contacts { get; set; }

    public DbSet<Language> Languages { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<ProductTranslation> ProductTranslations { get; set; }

    public DbSet<Promotion> Promotions { get; set; }


    public DbSet<Transaction> Transactions { get; set; }
}