using MidasShopSolution.ViewModels.Catalog.Products;
using MidasShopSolution.ViewModels.Catalog.ProductImages;
using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.Api.Application.Catalog.Products;

public interface IManageProductService
{
    Task<int> Create(ProductCreateRequest request);
    Task<int> Update(ProductUpdateRequest request);
    Task<int> Delete(int productId);
    Task<ProductViewModel> GetById(int productId);

    Task<bool> UpdatePrice(int productId, decimal newPrice);

    Task<bool> UpdateStock(int productId, int addedQuantity);

    Task AddViewCount(int productId);

    Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

    Task<int> AddImage(int productId, ProductImageCreateRequest request);
    Task<int> RemoveImage(int imageId);
    Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);
    Task<ProductImageViewModel> GetImageById(int imageId);
    Task<List<ProductImageViewModel>> GetListImages(int productId);
}