using Refit;
using MidasShopSolution.ViewModels.Categories;

namespace MidasShopSolution.CustomerSite.Services
{
    public interface ICategoryApiClient
    {
        [Get("/api/Categories")]
        Task<List<CategoryDto>> GetAll();
        [Get("/api/Categories/{id}")]
        Task<CategoryDto> GetById(int id);
    }
}