using MidasShopSolution.Application.Catalog.Products.Dtos;
using MidasShopSolution.Application.Dtos;

namespace MidasShopSolution.Application.Catalog.Products;

public class PublicProductService : IPublicProductService
{
    public PagedViewModel<ProductViewModel> GetAllByCategoryId(int categoryId, int pageIndex, int pageSize)
    {
        throw new NotImplementedException();
    }
}