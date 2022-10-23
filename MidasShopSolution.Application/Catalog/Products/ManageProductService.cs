using Microsoft.EntityFrameworkCore;
using MidasShopSolution.Data.EF;
using MidasShopSolution.Data.Entites;
using MidasShopSolution.Utilities.Exceptions;
using MidasShopSolution.ViewModels.Catalog.Products;
using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.Application.Catalog.Products;

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
            ProductTranslations = new List<ProductTranslation>()
            {
                new ProductTranslation()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Details = request.Details,
                    SeoDescription = request.SeoDescription,
                    SeoAlias = request.SeoAlias,
                    SeoTitle = request.SeoTitle,
                    LanguageId = request.LanguageId
                }
            }
        };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product.Id;
    }

    public async Task<int> Update(ProductUpdateRequest request)
    {
        var product = await _context.Products.FindAsync(request.Id);
        var productTranslations = await _context.ProductTranslations.FirstOrDefaultAsync(pt =>
            pt.ProductId == request.Id && pt.LanguageId == request.LanguageId);
        if (product == null || productTranslations == null)
            throw new MidasShopException($"Cannot find a product with id: {request.Id}");

        productTranslations.Name = request.Name;
        productTranslations.Description = request.Description;
        productTranslations.Details = request.Details;
        productTranslations.SeoDescription = request.SeoDescription;
        productTranslations.SeoAlias = request.SeoAlias;
        productTranslations.SeoTitle = request.SeoTitle;
        productTranslations.LanguageId = request.LanguageId;
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(int ProductId)
    {
        var product = await _context.Products.FindAsync(ProductId);
        if (product == null) throw new MidasShopException($"Cannot find a product with Id: {ProductId}");
        _context.Products.Remove(product);
        return await _context.SaveChangesAsync();
    }

    public async Task<ProductViewModel> GetById(int productId, string languageId)
    {
        var product = await _context.Products.FindAsync(productId);
        var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(pt => pt.ProductId == productId
            && pt.LanguageId == languageId);

        var productViewModel = new ProductViewModel()
        {
            Id = product.Id,
            DateCreated = product.DateCreated,
            OriginalPrice = product.OriginalPrice,
            Price = product.Price,
            Stock = product.Stock,
            ViewCount = product.ViewCount,

            LanguageId = productTranslation.LanguageId,
            Description = productTranslation != null ? productTranslation.Description : null,
            Details = productTranslation != null ? productTranslation.Details : null,
            Name = productTranslation != null ? productTranslation.Name : null,
            SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
            SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
            SeoTitle = productTranslation != null ? productTranslation.SeoTitle : null
        };
        return productViewModel;
    }

    public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
    {
        // 1. Select join
        var query = from p in _context.Products
                    join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                    join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                    join c in _context.Categories on pic.CategoryId equals c.Id
                    select new { p, pt, pic };

        // 2. Filter
        if (!string.IsNullOrEmpty(request.Keyword))
            query = query.Where(x => x.pt.Name.Contains(request.Keyword));
        if (request.CategoryIds.Count > 0)
        {
            query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
        }

        // 3. Paging
        int totalRow = await query.CountAsync();

        var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                Name = x.pt.Name,
                DateCreated = x.p.DateCreated,
                Description = x.pt.Description,
                Details = x.pt.Details,
                LanguageId = x.pt.LanguageId,
                OriginalPrice = x.p.OriginalPrice,
                Price = x.p.Price,
                SeoAlias = x.pt.SeoAlias,
                SeoDescription = x.pt.SeoDescription,
                SeoTitle = x.pt.SeoTitle,
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