using MidasShopSolution.Application.Catalog.Products.Dtos;
using MidasShopSolution.Application.Dtos;

namespace MidasShopSolution.Application.Catalog.Products;

public interface IManageProductService
{
    Task<int> Create(ProductCreateRequest request);
    Task<int> Update(ProductEditRequest request);

    Task<int> Delete(int ProductId);

    Task<List<ProductViewModel>> GetAll();
    
    Task<PagedViewModel<ProductViewModel>> GetAllPaging(string keyword, int pageIndex, int pageSize);
}