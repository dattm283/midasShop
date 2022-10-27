namespace MidasShopSolution.Data.Entities;

public class Product
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public int Stock { get; set; }
    public int ViewCount { get; set; }
    public DateTime DateCreated { get; set; }
    public string Name { set; get; }
    public string Description { set; get; }
    public string Details { set; get; }
    public string SeoDescription { set; get; }
    public string SeoTitle { set; get; }
    public string SeoAlias { get; set; }
    public bool? IsFeatured { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
    public List<Category> Categories { get; set; }
    public List<Cart> Carts { get; set; }
    public List<ProductImage> ProductImages { get; set; }
}