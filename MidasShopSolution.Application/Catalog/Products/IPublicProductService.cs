using MidasShopSolution.ViewModels.Catalog.Products;
using MidasShopSolution.ViewModels.Catalog.Products.Public;
using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.Application.Catalog.Products;

public interface IPublicProductService
{
    Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request);
}