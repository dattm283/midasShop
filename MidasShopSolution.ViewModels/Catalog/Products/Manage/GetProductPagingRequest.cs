using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.ViewModels.Catalog.Products.Manage;

public class GetProductPagingRequest: PagingRequestBase
{
    public  string Keyword { get; set; }
    public List<int> CategoryIds { get; set; }
}