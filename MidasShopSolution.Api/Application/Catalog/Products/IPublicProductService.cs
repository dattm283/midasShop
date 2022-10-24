using MidasShopSolution.ViewModels.Catalog.Products;
using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.Api.Application.Catalog.Products;

public interface IPublicProductService
{
    Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);
}