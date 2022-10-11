using MidasShopSolution.Data.Enums;

namespace MidasShopSolution.Data.Entites;

public class Promotion
{
    public int Id { set; get; }
    public DateTime FromDate { set; get; }
    public DateTime ToDate { set; get; }
    public bool ApplyForAll { set; get; }
    public int? DiscountPercent { set; get; }
    public decimal? DiscountAmount { set; get; }
    public string ProductIds { set; get; }
    public string ProductCategoryIds { set; get; }
    public Status Status { set; get; }
    public string Name { set; get; }
}