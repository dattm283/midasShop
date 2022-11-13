using Microsoft.AspNetCore.Http;

namespace MidasShopSolution.ViewModels.Products;

public class ProductCreateRequest
{
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public int Stock { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Details { get; set; }
    public string SeoDescription { get; set; }
    public string SeoTitle { get; set; }
    public string SeoAlias { get; set; }
    public bool IsFeatured { get; set; }
    public IFormFile Images { get; set; }
}