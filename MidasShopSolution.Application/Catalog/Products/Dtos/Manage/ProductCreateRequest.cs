namespace MidasShopSolution.Application.Catalog.Products.Dtos.Manage;

public class ProductCreateRequest
{
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public int Stock { get; set; }
    public int ViewCount { get; set; }
    public DateTime DateCreated { get; set; }

    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Details { get; set; }
    public string SeoDescription { get; set; }
    public string SeoTitle { get; set; }

    public string SeoAlias { get; set; }
    public string LanguageId { get; set; }
}