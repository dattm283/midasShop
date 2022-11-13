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

    public async Task<int> Create(CategoryDto request)
    {
        var category = new Category()
        {
            Id = request.Id,
            Name = request.Name,
            ParentId = request.ParentId
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category.Id;
    }

     public async Task<int> Update(CategoryDto request)
    {
        var category = await _context.Categories.FindAsync(request.Id);
        if (category == null)
            throw new MidasShopException($"Cannot find a category with id: {request.Id}");

        category.Name = request.Name;
        category.ParentId = request.ParentId;
       
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
