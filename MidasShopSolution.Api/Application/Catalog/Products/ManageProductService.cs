using Microsoft.EntityFrameworkCore;
using MidasShopSolution.Data.EF;
using MidasShopSolution.Data.Entites;
using MidasShopSolution.Api.Utilities.Exceptions;
using MidasShopSolution.ViewModels.Catalog.Products;
using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.Api.Application.Catalog.Products;

public class ManageProductService : IManageProductService
{
    private readonly MidasShopDbContext _context;

    public ManageProductService(MidasShopDbContext context)
    {
        _context = context;
    }

    public async Task AddViewCount(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        product.ViewCount += 1;
        await _context.SaveChangesAsync();
    }

    public async Task<int> Create(ProductCreateRequest request)
    {
        var product = new Product()
        {
            Price = request.Price,
            OriginalPrice = request.OriginalPrice,
            Stock = request.Stock,
            ViewCount = 0,
            DateCreated = DateTime.Now,
            Name = request.Name,
            Description = request.Description,
            Details = request.Details,
            SeoDescription = request.SeoDescription,
            SeoAlias = request.SeoAlias,
            SeoTitle = request.SeoTitle
        };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product.Id;
    }

    public async Task<int> Update(ProductUpdateRequest request)
    {
        var product = await _context.Products.FindAsync(request.Id);
        if (product == null)
            throw new MidasShopException($"Cannot find a product with id: {request.Id}");

        product.Name = request.Name;
        product.Description = request.Description;
        product.Details = request.Details;
        product.SeoDescription = request.SeoDescription;
        product.SeoAlias = request.SeoAlias;
        product.SeoTitle = request.SeoTitle;
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) throw new MidasShopException($"Cannot find a product with Id: {productId}");
        _context.Products.Remove(product);
        return await _context.SaveChangesAsync();
    }

    public async Task<ProductViewModel> GetById(int productId)
    {
        var product = await _context.Products.FindAsync(productId);

        var productViewModel = new ProductViewModel()
        {
            Id = product.Id,
            DateCreated = product.DateCreated,
            OriginalPrice = product.OriginalPrice,
            Price = product.Price,
            Stock = product.Stock,
            ViewCount = product.ViewCount,

            Description = product.Description,
            Details = product.Details,
            Name = product.Name,
            SeoAlias = product.SeoAlias,
            SeoDescription = product.SeoDescription,
            SeoTitle = product.SeoTitle
        };
        return productViewModel;
    }

    public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
    {
        // 1. Select join
        var query = from p in _context.Products
                    select new { p };

        // 2. Filter
        if (!string.IsNullOrEmpty(request.Keyword))
            query = query.Where(x => x.p.Name.Contains(request.Keyword));
         // if (request.CategoryIds.Count > 0)
         // {
         //     query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
         // }

        // 3. Paging
        int totalRow = await query.CountAsync();

        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                DateCreated = x.p.DateCreated,
                Description = x.p.Description,
                Details = x.p.Details,
                OriginalPrice = x.p.OriginalPrice,
                Price = x.p.Price,
                SeoAlias = x.p.SeoAlias,
                SeoDescription = x.p.SeoDescription,
                SeoTitle = x.p.SeoTitle,
                ViewCount = x.p.ViewCount
            }).ToListAsync();

        // 4. Select and projection
        var pagedResult = new PagedResult<ProductViewModel>()
        {
            TotalRecord = totalRow,
            //Items = await data.ToListAsync();
        };
        return pagedResult;
    }

    public async Task<bool> UpdatePrice(int productId, decimal newPrice)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) throw new MidasShopException($"Cannot find a product with id: {productId}");

        product.Price = newPrice;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateStock(int productId, int addedQuantity)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) throw new MidasShopException($"Cannot find a product with id: {productId}");
        product.Stock += addedQuantity;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateImage(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) throw new MidasShopException($"Cannot find a product with id: {productId}");

        return await _context.SaveChangesAsync() > 0;
    }
}