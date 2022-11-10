using MidasShopSolution.Data.Enums;

namespace MidasShopSolution.ViewModels.Categories
{
    public class CreateCategoryRequestDto
    {
        public int SortOrder { get; set; }
        public bool IsShowOnHome { get; set; }
        public int? ParentId { get; set; }
        public string Name { set; get; }
        public string SeoDescription { get; set; }
        public Status Status { get; set; }
    }
}