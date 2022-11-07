using Refit;
using MidasShopSolution.ViewModels.Products;
using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.CustomerSite.Services
{
    public interface IProductApiClient
    {
        [Get("/api/Products/featured/{take}")]
        Task<List<ProductDto>> GetFeaturedProducts(int take);

        [Get("/api/Products/category")]
        Task<GetProductsByCategoryDto> GetAllByCategoryId(GetPublicProductPagingRequest request);
        [Get("/api/Products/{productId}")]
        Task<ProductDto> GetById(int productId);
    }
}