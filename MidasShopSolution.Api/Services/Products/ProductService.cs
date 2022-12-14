using Microsoft.EntityFrameworkCore;
using MidasShopSolution.Data.EF;
using MidasShopSolution.Data.Entities;
using MidasShopSolution.Api.Utilities.Exceptions;
using MidasShopSolution.ViewModels.Products;
using MidasShopSolution.ViewModels.ProductImages;
using MidasShopSolution.ViewModels.Common;
using MidasShopSolution.ViewModels.Categories;


namespace MidasShopSolution.Api.Services.Products;

public class ProductService : IProductService
{
    private readonly MidasShopDbContext _context;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ProductService(MidasShopDbContext context, IWebHostEnvironment hostingEnvironment)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task AddViewCount(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product != null)
        {
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }
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
            SeoDescription = request.SeoDescription != null ? request.SeoDescription : request.Description,
            SeoAlias = request.SeoAlias != null ? request.SeoAlias : request.Description,
            SeoTitle = request.SeoTitle != null ? request.SeoTitle : request.Name,
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

    public async Task<ProductDto> GetById(int productId)
    {
        var product = await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == productId);

        var productViewModel = new ProductDto()
        {
            Id = product.Id,
            Name = product.Name,
            DateCreated = product.DateCreated,
            Description = product.Description,
            Details = product.Details,
            OriginalPrice = product.OriginalPrice,
            Price = product.Price,
            Stock = product.Stock,
            SeoAlias = product.SeoAlias,
            SeoDescription = product.SeoDescription,
            SeoTitle = product.SeoTitle,
            ViewCount = product.ViewCount,
            IsFeatured = product.IsFeatured,
            Images = product.ProductImages.FindAll(p => p.IsDefault == true)
        };
        return productViewModel;
    }
    public async Task<PagedResult<ProductDto>> GetAllProduct(PagingRequestBase request)
    {
        // 1. Select join
        var query = await _context.Products.Include(p => p.Categories).Include(p => p.ProductImages).ToListAsync();

        // 3. Paging
        int totalRow = query.Count();

        var data = query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                DateCreated = x.DateCreated,
                Description = x.Description,
                Details = x.Details,
                OriginalPrice = x.OriginalPrice,
                Price = x.Price,
                Stock = x.Stock,
                SeoAlias = x.SeoAlias,
                SeoDescription = x.SeoDescription,
                SeoTitle = x.SeoTitle,
                ViewCount = x.ViewCount,
                IsFeatured = x.IsFeatured,
                Images = x.ProductImages.FindAll(p => p.IsDefault == true)
            }).ToList();

        // 4. Select and projection
        var pagedResult = new PagedResult<ProductDto>()
        {
            TotalRecord = totalRow,
            Items = data
        };
        return pagedResult;
    }

    public async Task<PagedResult<ProductDto>> GetAllPagingByKeyword(GetManageProductPagingRequest request)
    {
        // 1. Select join
        var query = await _context.Products.Include(p => p.Categories).ToListAsync();

        // 2.Filter
        if (!string.IsNullOrEmpty(request.Keyword))
            query = query.Where(p => request.Keyword.Any(keyword => p.Name.Contains(keyword))).ToList();

        if (request.CategoryIds.Count > 0)
        {
            // query = query.Where(x => request.CategoryIds.Contains(x.p.Categories.FindAll()));
            query = query.Where(p => p.Categories.Any(c => request.CategoryIds.Any(categoryId => c.Id == categoryId))).ToList();
        }

        // 3. Paging
        int totalRow = query.Count();

        var data = query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                DateCreated = x.DateCreated,
                Description = x.Description,
                Details = x.Details,
                OriginalPrice = x.OriginalPrice,
                Price = x.Price,
                Stock = x.Stock,
                SeoAlias = x.SeoAlias,
                SeoDescription = x.SeoDescription,
                SeoTitle = x.SeoTitle,
                ViewCount = x.ViewCount,
                IsFeatured = x.IsFeatured,
                Images = x.ProductImages.FindAll(p => p.IsDefault == true)
            }).ToList();

        // 4. Select and projection
        var pagedResult = new PagedResult<ProductDto>()
        {
            TotalRecord = totalRow,
            Items = data
            //Items = await data.ToListAsync();
        };
        return pagedResult;
    }

    public async Task<GetProductsByCategoryDto> GetAllByCategoryId(GetPublicProductPagingRequest request)
    {
        // 1. Select join
        var query = await _context.Products.Include(p => p.Categories).Include(p => p.ProductImages).ToListAsync();

        var categoryQuery = await _context.Categories.FindAsync(request.CategoryId);
        // 2. Filter
        if (categoryQuery != null)
        {
            // query = query.Where(x => request.CategoryIds.Contains(x.p.Categories.FindAll()));
            query = query.Where(p => p.Categories.Any(c => c.Id == request.CategoryId)).ToList();
        }

        // 3. Paging
        int totalRow = query.Count();

        var data = query.Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                DateCreated = x.DateCreated,
                Description = x.Description,
                Details = x.Details,
                OriginalPrice = x.OriginalPrice,
                Price = x.Price,
                Stock = x.Stock,
                SeoAlias = x.SeoAlias,
                SeoDescription = x.SeoDescription,
                SeoTitle = x.SeoTitle,
                ViewCount = x.ViewCount,
                IsFeatured = x.IsFeatured,
                Images = x.ProductImages.FindAll(p => p.IsDefault == true)
            }).ToList();

        // 4. Select and projection
        var pagedResult = new PagedResult<ProductDto>()
        {
            TotalRecord = totalRow,
            Items = data
        };
        var categoryData = new CategoryDto()
        {
            Id = categoryQuery.Id,
            Name = categoryQuery.Name,
            ParentId = categoryQuery.ParentId
        };
        var productsByCategoryList = new GetProductsByCategoryDto()
        {
            Category = categoryData,
            Products = pagedResult
        };
        return productsByCategoryList;
    }
    public async Task<List<ProductDto>> GetFeaturedProducts(int take)
    {
        // 1. Select join + 2. Filter
        var query = await _context.Products.Include(p => p.ProductImages).Where(p => p.IsFeatured == true).ToListAsync();

        // 3. Paging

        var data = query.OrderByDescending(p => p.DateCreated)
            .Take(take)
            .Select(x => new ProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                DateCreated = x.DateCreated,
                Description = x.Description,
                Details = x.Details,
                OriginalPrice = x.OriginalPrice,
                Price = x.Price,
                Stock = x.Stock,
                SeoAlias = x.SeoAlias,
                SeoDescription = x.SeoDescription,
                SeoTitle = x.SeoTitle,
                ViewCount = x.ViewCount,
                Images = x.ProductImages.FindAll(p => p.IsDefault == true)
            }).ToList();

        // 4. Select and projection
        // var pagedResult = new PagedResult<ProductDto>()
        // {
        //     TotalRecord = data.Count(),
        //     Items = data
        // };
        return data;
    }
    public async Task<bool> UpdatePrice(int productId, decimal newPrice)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) throw new MidasShopException($"Cannot find a product with id: {productId}");

        product.Price = newPrice;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateStar(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        var commentList = await _context.Comments.Where(c => c.ProductId == productId).ToListAsync();
        if (product == null) throw new MidasShopException($"Cannot find a product with id: {productId}");
        product.ViewCount = Convert.ToInt32((from item in commentList
                                             select item.Rate).Average());
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
    public async Task<bool> CategoryAssign(int id, CategoryAssignRequest request)
    {
        var product = await _context.Products.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            throw new MidasShopException($"S???n ph???m v???i id {id} kh??ng t???n t???i");
        }
        product.Categories = new List<Category>();
        foreach (var listItem in request.Categories)
        {
            var categoryItem = await _context.Categories.FindAsync(listItem.Id);
            if (categoryItem != null && listItem.Selected == true)
            {
                product.Categories.Add(categoryItem);
            }
        }

        // var result = new ApiResult<bool>()
        // {
        //     IsSuccessed = true,
        //     Message = ""
        //     // ResultObj
        // };
        return await _context.SaveChangesAsync() > 0;
    }
}