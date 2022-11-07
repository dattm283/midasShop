using MidasShopSolution.Data.EF;
using MidasShopSolution.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;

namespace MidasShopSolution.Api.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly MidasShopDbContext _context;
    public CategoryService(MidasShopDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> GetAll()
    {
        var category = await _context.Categories.ToListAsync();
        return category.Select(c => new CategoryDto()
        {
            Id = c.Id,
            Name = c.Name,
            ParentId = c.ParentId
        }).ToList();
    }

    public async Task<CategoryDto> GetById(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        return new CategoryDto()
        {
            Id = category.Id,
            Name = category.Name,
            ParentId = category.ParentId
        };
    }
}
