using MidasShopSolution.Application.Catalog.Products.Dtos;
using MidasShopSolution.Application.Catalog.Products.Dtos.Manage;
using MidasShopSolution.Application.Dtos;

namespace MidasShopSolution.Application.Catalog.Products;

public interface IManageProductService
{
    Task<int> Create(ProductCreateRequest request);
    Task<int> Update(ProductUpdateRequest request);

    Task<int> Delete(int ProductId);
    
    Task<bool> UpdatePrice(int ProductId, decimal newPrice);

    Task<bool> UpdateStock(int ProductId, int addedQuantity);

    Task AddViewCount(int ProductId);
    Task<List<ProductViewModel>> GetAll();
    
    Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request);
}