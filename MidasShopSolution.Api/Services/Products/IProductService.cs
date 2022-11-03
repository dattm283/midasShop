using MidasShopSolution.ViewModels.Products;
using MidasShopSolution.ViewModels.ProductImages;
using MidasShopSolution.ViewModels.Common;
using MidasShopSolution.Data.Entities;

namespace MidasShopSolution.Api.Services.Products;

public interface IProductService
{
    Task<int> Create(ProductCreateRequest request);
    Task<int> Update(ProductUpdateRequest request);
    Task<int> Delete(int productId);
    Task<ProductViewModel> GetById(int productId);
    Task<bool> UpdatePrice(int productId, decimal newPrice);
    Task<bool> UpdateStock(int productId, int addedQuantity);
    Task AddViewCount(int productId);
    Task<PagedResult<ProductViewModel>> GetAllPagingByKeyword(GetManageProductPagingRequest request);
    Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);
    // Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);
    Task<Product> CategoryAssign(int id, CategoryAssignRequest request);

    Task<int> AddImage(int productId, ProductImageCreateRequest request);
    Task<int> RemoveImage(int imageId);
    Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);
    Task<ProductImageViewModel> GetImageById(int imageId);
    Task<List<ProductImageViewModel>> GetListImages(int productId);

}