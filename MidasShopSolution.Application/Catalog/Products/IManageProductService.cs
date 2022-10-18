using MidasShopSolution.Application.Catalog.Products.Dtos;
using MidasShopSolution.Application.Catalog.Products.Dtos.Manage;
using MidasShopSolution.Application.Dtos;

namespace MidasShopSolution.Application.Catalog.Products;

public interface IManageProductService
{
    Task<int> Create(ProductCreateRequest request);
    Task<int> Update(ProductUpdateRequest request);

    Task<int> Delete(int productId);
    
    Task<bool> UpdatePrice(int productId, decimal newPrice);

    Task<bool> UpdateStock(int productId, int addedQuantity);

    Task AddViewCount(int productId);
    
    Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request);
}