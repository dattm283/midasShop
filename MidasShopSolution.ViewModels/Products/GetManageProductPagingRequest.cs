using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.ViewModels.Products;

public class GetManageProductPagingRequest : PagingRequestBase
{
    public string Keyword { get; set; }
    public List<int> CategoryIds { get; set; }
}