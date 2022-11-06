using MidasShopSolution.ViewModels.Products;
using MidasShopSolution.ViewModels.ProductImages;
using MidasShopSolution.ViewModels.Common;
using MidasShopSolution.Data.Entities;

namespace MidasShopSolution.Api.Services.Products;

public interface IProductService
{
    Task<PagedResult<ProductDto>> GetAllPagingByKeyword(GetManageProductPagingRequest request);
    Task<PagedResult<ProductDto>> GetAllByCategoryId(GetPublicProductPagingRequest request);
    Task<List<ProductDto>> GetFeaturedProducts(int take);
    Task<ProductDto> GetById(int productId);
    Task<int> Create(ProductCreateRequest request);
    Task<int> Update(ProductUpdateRequest request);
    Task<bool> UpdatePrice(int productId, decimal newPrice);
    Task<bool> UpdateStock(int productId, int addedQuantity);
    Task AddViewCount(int productId);
    Task<bool> CategoryAssign(int id, CategoryAssignRequest request);
    Task<int> Delete(int productId);

    Task<ProductImageViewModel> GetImageById(int imageId);
    Task<List<ProductImageViewModel>> GetListImages(int productId);
    Task<int> AddImage(int productId, ProductImageCreateRequest request);
    Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);
    Task<int> RemoveImage(int imageId);

}