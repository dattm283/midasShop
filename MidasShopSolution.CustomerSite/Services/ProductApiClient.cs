using MidasShopSolution.ViewModels.Products;
using MidasShopSolution.ViewModels.Common;
using Newtonsoft.Json;

namespace MidasShopSolution.CustomerSite.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly IProductApiClient _ProductApiClient;
        public ProductApiClient(IProductApiClient productApiClient)
        {
            _ProductApiClient = productApiClient;
        }
        public async Task<List<ProductDto>> GetFeaturedProducts(int take)
        {
            return await _ProductApiClient.GetFeaturedProducts(take);
        }
        public async Task<GetProductsByCategoryDto> GetAllByCategoryId(GetPublicProductPagingRequest request)
        {
            return await _ProductApiClient.GetAllByCategoryId(request);
        }
        public async Task<ProductDto> GetById(int productId)
        {
            return await _ProductApiClient.GetById(productId);
        }
    }
}