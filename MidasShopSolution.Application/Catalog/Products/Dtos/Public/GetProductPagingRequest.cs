using MidasShopSolution.Application.Dtos;

namespace MidasShopSolution.Application.Catalog.Products.Dtos.Public;

public class GetProductPagingRequest: PagingRequestBase
{
    public int CategoryId { get; set; }
}