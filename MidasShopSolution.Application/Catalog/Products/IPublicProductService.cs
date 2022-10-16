using MidasShopSolution.Application.Catalog.Products.Dtos;
using MidasShopSolution.Application.Dtos;

namespace MidasShopSolution.Application.Catalog.Products;

public interface IPublicProductService
{
    PagedViewModel<ProductViewModel> GetAllByCategoryId(int categoryId, int pageIndex, int pageSize);
}