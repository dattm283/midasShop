using MidasShopSolution.Application.Catalog.Products.Dtos;
using MidasShopSolution.Application.Dtos;
using MidasShopSolution.Data.EF;
using MidasShopSolution.Data.Entites;

namespace MidasShopSolution.Application.Catalog.Products;

public class MangageProductService: IManageProductService
{
    private readonly MidasShopDbContext _context;
    public MangageProductService(MidasShopDbContext context)
    {
        _context = context;
    }
    public async Task<int> Create(ProductCreateRequest request)
    {
        var product = new Product()
        {
            Price = request.Price,
        };
        _context.Products.Add(product);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Update(ProductEditRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Delete(int ProductId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ProductViewModel>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<PagedViewModel<ProductViewModel>> GetAllPaging(string keyword, int pageIndex, int pageSize)
    {
        throw new NotImplementedException();
    }
}