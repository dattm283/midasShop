using MidasShopSolution.Data.EF;
using MidasShopSolution.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;
using MidasShopSolution.Data.Entities;
using MidasShopSolution.Api.Utilities.Exceptions;


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

    public async Task<int> Create(CreateCategoryRequestDto request)
    {
        var category = new Category()
        {
            Name = request.Name,
            ParentId = request.ParentId,
            SortOrder = request.SortOrder,
            IsShowOnHome = request.IsShowOnHome,
            Status = request.Status,
            SeoAlias = request.Name,
            SeoTitle = request.Name,
            SeoDescription = request.SeoDescription
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category.Id;
    }

    public async Task<int> Update(int categoryId, CreateCategoryRequestDto request)
    {
        var category = await _context.Categories.FindAsync(categoryId);
        if (category == null)
            throw new MidasShopException($"Cannot find a category with id: {categoryId}");

        category.Name = request.Name;
        category.ParentId = request.ParentId;
        category.SortOrder = request.SortOrder;
        category.IsShowOnHome = request.IsShowOnHome;
        category.Status = request.Status;
        category.SeoAlias = request.Name;
        category.SeoDescription = request.SeoDescription;

        return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(int categoryId)
    {
        var category = await _context.Categories.FindAsync(categoryId);
        if (category == null) throw new MidasShopException($"Cannot find a category with Id: {categoryId}");

        _context.Categories.Remove(category);
        return await _context.SaveChangesAsync();
    }
}
