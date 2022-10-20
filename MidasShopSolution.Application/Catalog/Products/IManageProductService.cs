

using MidasShopSolution.ViewModels.Catalog.Products;
using MidasShopSolution.ViewModels.Catalog.Products.Manage;
using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.Application.Catalog.Products;

public interface IManageProductService
{
    Task<int> Create(ProductCreateRequest request);
    Task<int> Update(ProductUpdateRequest request);

    Task<int> Delete(int productId);
    
    Task<bool> UpdatePrice(int productId, decimal newPrice);

    Task<bool> UpdateStock(int productId, int addedQuantity);

    Task AddViewCount(int productId);
    
    Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
}