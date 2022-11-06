using Refit;
using MidasShopSolution.ViewModels.Products;
using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.CustomerSite.Services
{
    public interface IProductApiClient
    {
        [Get("/api/Products/featured/{take}")]
        Task<List<ProductDto>> GetFeaturedProducts(int take);
        Task<PagedResult<ProductDto>> GetAllByCategoryId(GetPublicProductPagingRequest request);
    }
}