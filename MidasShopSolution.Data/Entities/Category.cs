using MidasShopSolution.Data.Enums;

namespace MidasShopSolution.Data.Entities;

public class Category
{
    public int Id { get; set; }
    public int SortOrder { get; set; }
    public bool IsShowOnHome { get; set; }
    public int? ParentId { get; set; }
    public string Name { set; get; }
    public string SeoDescription { set; get; }
    public string SeoTitle { set; get; }
    public string SeoAlias { set; get; }
    public Status Status { get; set; }
    public List<Product> Products { get; set; }
}