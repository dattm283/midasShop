using MidasShopSolution.ViewModels.Categories;
using MidasShopSolution.ViewModels.Common;
using MidasShopSolution.ViewModels.Products;

namespace MidasShopSolution.ViewModels.Products
{
    public class GetProductsByCategoryDto
    {
        public CategoryDto Category { get; set; }
        public PagedResult<ProductDto> Products { get; set; }
    }
}