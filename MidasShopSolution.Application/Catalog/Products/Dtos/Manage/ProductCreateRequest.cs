namespace MidasShopSolution.Application.Catalog.Products.Dtos.Manage;

public class ProductCreateRequest
{
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public int Stock { get; set; }
    public int ViewCount { get; set; }
    public DateTime DateCreated { get; set; }
    
    public int ProductId { set; get; }
    public string Name { set; get; }
    public string Description { set; get; }
    public string Details { set; get; }
    public string SeoDescription { set; get; }
    public string SeoTitle { set; get; }

    public string SeoAlias { get; set; }
    public string LanguageId { set; get; }
}