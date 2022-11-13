using MidasShopSolution.ViewModels.Categories;

namespace MidasShopSolution.Api.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAll();
        Task<CategoryDto> GetById(int id);
        Task<int> Create(CreateCategoryRequestDto request);
        Task<int> Update(int categoryId, CreateCategoryRequestDto request);
        Task<int> Delete(int categoryId);
    }
}