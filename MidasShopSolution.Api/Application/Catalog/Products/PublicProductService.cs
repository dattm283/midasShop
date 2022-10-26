using Microsoft.EntityFrameworkCore;
using MidasShopSolution.Data.EF;
using MidasShopSolution.ViewModels.Catalog.Products;
using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.Api.Application.Catalog.Products;

public class PublicProductService : IPublicProductService
{
    private readonly MidasShopDbContext _context;

    public PublicProductService(MidasShopDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request)
    {
        // 1. Select join
        var query = from p in _context.Products
                    select new { p };

        // 2. Filter
        // if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
        // {
        //     query = query.Where(p => p.pic.CategoryId == request.CategoryId);
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
}