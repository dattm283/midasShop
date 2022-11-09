using MidasShopSolution.ViewModels.Categories;

namespace MidasShopSolution.Api.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto> GetById(int id);
        Task<int> Create(CategoryDto request);
        Task<int> Update(CategoryDto request);
        Task<int> Delete(int categoryId);
    }
}