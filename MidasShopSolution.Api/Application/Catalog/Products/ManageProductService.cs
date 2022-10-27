using Microsoft.EntityFrameworkCore;
using MidasShopSolution.Data.EF;
using MidasShopSolution.Data.Entities;
using MidasShopSolution.Api.Utilities.Exceptions;
using MidasShopSolution.ViewModels.Catalog.Products;
using MidasShopSolution.ViewModels.Catalog.ProductImages;
using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.Api.Application.Catalog.Products;

public class ManageProductService : IManageProductService
{
    private readonly MidasShopDbContext _context;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ManageProductService(MidasShopDbContext context, IWebHostEnvironment hostingEnvironment)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;
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
            SeoTitle = request.SeoTitle,
            IsFeatured = request.IsFeatured
        };
        if (request.Images != null)
        {
            product.ProductImages = new List<ProductImage>()
                {
                new ProductImage()
                {
                    Caption = "Thumbnail image",
                    DateCreated = DateTime.Now,
                    FileSize = request.Images.Length,
                    ImagePath = await UploadFile(request.Images),
                    IsDefault = true,
                    SortOrder = 1,
                }
                };
        }
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
        product.IsFeatured = request.IsFeatured;
        if (request.Images != null)
        {
            var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);
            if (thumbnailImage != null)
            {
                thumbnailImage.FileSize = request.Images.Length;
                thumbnailImage.ImagePath = await UploadFile(request.Images);
                _context.ProductImages.Update(thumbnailImage);
            }
        }
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) throw new MidasShopException($"Cannot find a product with Id: {productId}");

        // Remove Images
        var images = _context.ProductImages.Where(i => i.ProductId == productId);
        foreach (var image in images)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, image.ImagePath);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

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

    private async Task<string> UploadFile(IFormFile file)
    {
        string fileName = null;
        if (file != null)
        {
            string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
            string filePath = Path.Combine(uploadDir, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
        }

        return fileName;
    }

    public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
    {
        var productImage = new ProductImage()
        {
            Caption = request.Caption,
            DateCreated = DateTime.Now,
            IsDefault = request.IsDefault,
            ProductId = productId,
            SortOrder = request.SortOrder
        };

        if (request.ImageFile != null)
        {
            productImage.ImagePath = await this.UploadFile(request.ImageFile);
            productImage.FileSize = request.ImageFile.Length;
        }
        _context.ProductImages.Add(productImage);
        await _context.SaveChangesAsync();
        return productImage.Id;
    }

    public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
    {
        var productImage = await _context.ProductImages.FindAsync(imageId);
        if (productImage == null)
            throw new MidasShopException($"Cannot find an image with id {imageId}");

        if (request.ImageFile != null)
        {
            productImage.ImagePath = await this.UploadFile(request.ImageFile);
            productImage.FileSize = request.ImageFile.Length;
        }
        _context.ProductImages.Update(productImage);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> RemoveImage(int imageId)
    {
        var productImage = await _context.ProductImages.FindAsync(imageId);
        if (productImage == null)
            throw new MidasShopException($"Cannot find an image with id {imageId}");
        _context.ProductImages.Remove(productImage);
        return await _context.SaveChangesAsync();
    }

    public async Task<ProductImageViewModel> GetImageById(int imageId)
    {
        var image = await _context.ProductImages.FindAsync(imageId);
        if (image == null)
            throw new MidasShopException($"Cannot find an image with id {imageId}");

        var viewModel = new ProductImageViewModel()
        {
            Caption = image.Caption,
            DateCreated = image.DateCreated,
            FileSize = image.FileSize,
            Id = image.Id,
            ImagePath = image.ImagePath,
            IsDefault = image.IsDefault,
            ProductId = image.ProductId,
            SortOrder = image.SortOrder
        };
        return viewModel;
    }

    public async Task<List<ProductImageViewModel>> GetListImages(int productId)
    {
        return await _context.ProductImages.Where(x => x.ProductId == productId)
            .Select(i => new ProductImageViewModel()
            {
                Caption = i.Caption,
                DateCreated = i.DateCreated,
                FileSize = i.FileSize,
                Id = i.Id,
                ImagePath = i.ImagePath,
                IsDefault = i.IsDefault,
                ProductId = i.ProductId,
                SortOrder = i.SortOrder
            }).ToListAsync();
    }
}