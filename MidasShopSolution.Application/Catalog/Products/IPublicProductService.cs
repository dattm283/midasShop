using MidasShopSolution.Application.Catalog.Products.Dtos;
using MidasShopSolution.Application.Catalog.Products.Dtos.Public;
using MidasShopSolution.Application.Dtos;

namespace MidasShopSolution.Application.Catalog.Products;

public interface IPublicProductService
{
    Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request);
}