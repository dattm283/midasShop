using MidasShopSolution.Application.Dtos;

namespace MidasShopSolution.Application.Catalog.Products.Dtos.Manage;

public class GetProductPagingRequest: PagingRequestBase
{
    public  string Keyword { get; set; }
    public List<int> CategoryIds { get; set; }
}