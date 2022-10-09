using MidasShopSolution.Data.Enums;

namespace MidasShopSolution.Data.Entites;

public class Category
{
    public int Id { get; set; }
    public int SortOrder { get; set; }
    public bool IsShowOnHome { get; set; }
    public int? ParentId { get; set; }
    public Status status { get; set; }
    
    public List<ProductInCategory> ProductInCategories { get; set; }
    
    public List<CategoryTranslation> CategoryTranslations { get; set; }
}