using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MidasShopSolution.Data.Entites;
using MidasShopSolution.Data.Enums;

namespace MidasShopSolution.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppConfig>().HasData(
            new AppConfig()
            {
                Key = "HomeTitle",
                Value = "This is Home page of MidasShopSolution"
            },
            new AppConfig()
            {
                Key = "HomeKeyWord",
                Value = "This is Keyword of MidasShopSolution"
            },
            new AppConfig()
            {
                Key = "HomeDescription",
                Value = "This is Description of MidasShopSolution"
            }
        );

        modelBuilder.Entity<Language>().HasData(
            new Language()
            {
                Id = "vi-VN",
                Name = "Tiếng Việt",
                IsDefault = true
            },
            new Language()
            {
                Id = "en-US",
                Name = "English",
                IsDefault = false
            });

        modelBuilder.Entity<Category>().HasData(
            new Category()
            {
                Id = 1,
                IsShowOnHome = true,
                ParentId = null,
                SortOrder = 1,
                Status = Status.Active,
            },
            new Category()
            {
                Id = 2,
                IsShowOnHome = true,
                ParentId = null,
                SortOrder = 2,
                Status = Status.Active,
            }
        );

        modelBuilder.Entity<CategoryTranslation>().HasData(
            new CategoryTranslation()
            {
                Id = 1,
                CategoryId = 1,
                Name = "Áo nam",
                LanguageId = "vi-VN",
                SeoAlias = "ao-nam",
                SeoDescription = "Sản phẩm áo thời trang nam",
                SeoTitle = "Sản phẩm áo thời trang nam"
            },
            new CategoryTranslation()
            {
                Id = 2,
                CategoryId = 1,
                Name = "Men shirt",
                LanguageId = "en-US",
                SeoAlias = "men-shirt",
                SeoDescription = "The shirt products for men",
                SeoTitle = "The shirt products for men"
            },
            new CategoryTranslation()
            {
                Id = 3,
                CategoryId = 2,
                Name = "Áo nữ",
                LanguageId = "vi-VN",
                SeoAlias = "ao-nu",
                SeoDescription = "Sản phẩm áo thời trang nữ",
                SeoTitle = "Sản phẩm áo thời trang nữ"
            },
            new CategoryTranslation()
            {
                Id = 4,
                CategoryId = 2,
                Name = "Women shirt",
                LanguageId = "en-US",
                SeoAlias = "women-shirt",
                SeoDescription = "The shirt products for women",
                SeoTitle = "The shirt products for women"
            }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product()
            {
                Id = 1,
                DateCreated = DateTime.Now,
                OriginalPrice = 100000,
                Price = 200000,
                Stock = 0,
                ViewCount = 0,
            });
        modelBuilder.Entity<ProductTranslation>().HasData(
            new ProductTranslation()
            {
                Id = 1,
                ProductId = 1,
                Name = "Áo sơ mi nam trắng Việt Tiến",
                LanguageId = "vi-VN",
                SeoAlias = "ao-so-mi-nam-trang-viet-tien",
                SeoDescription = "Áo sơ mi nam trắng Việt Tiến",
                SeoTitle = "Áo sơ mi nam trắng Việt Tiến",
                Details = "Áo sơ mi nam trắng Việt Tiến",
                Description = "Áo sơ mi nam trắng Việt Tiến"
            },
            new ProductTranslation()
            {
                Id = 2,
                ProductId = 1,
                Name = "Viet Tien men white shirt",
                LanguageId = "en-US",
                SeoAlias = "viet-tien-men-white-shirt",
                SeoDescription = "Viet Tien men white shirt",
                SeoTitle = "Viet Tien men white shirt",
                Details = "Viet Tien men white shirt",
                Description = "Viet Tien men white shirt"
            });
        modelBuilder.Entity<ProductInCategory>().HasData(
            new ProductInCategory()
            {
                ProductId = 1,
                CategoryId = 1,
            });

        var roleId = new Guid("8256EE14-C826-4E80-83DE-D467EB4EE1E7");
        var adminId = new Guid("84D35D2A-8EEF-4497-95F7-3B32BDF64546");
        modelBuilder.Entity<Role>().HasData(new Role
        {
            Id = roleId,
            Name = "admin",
            NormalizedName = "admin",
            Description = "Administrator role"
        });

        var hasher = new PasswordHasher<User>();
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = adminId,
            UserName = "admin",
            NormalizedUserName = "admin",
            Email = "dattranadmin@gmail.com",
            NormalizedEmail = "dattranadmin@gmail.com",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
            SecurityStamp = string.Empty,
            FirstName = "Dat",
            LastName = "Tran",
            Dob = new DateTime(2001, 03, 28)
        });

        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
        {
            RoleId = roleId,
            UserId = adminId
        });
    }
}