using MidasShopSolution.ViewModels.Categories;

namespace MidasShopSolution.CustomerSite.Services
{
    public class CategoryApiClient : ICategoryApiClient
    {
        private readonly ICategoryApiClient _CategoryApiClient;
        public CategoryApiClient(ICategoryApiClient categoryApiClient)
        {
            _CategoryApiClient = categoryApiClient;
        }
        public async Task<List<CategoryDto>> GetAll()
        {
            return await _CategoryApiClient.GetAll();
        }

        public async Task<CategoryDto> GetById(int id)
        {
            return await _CategoryApiClient.GetById(id);
        }
    }
}