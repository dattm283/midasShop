using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.ViewModels.Products;

public class GetPublicProductPagingRequest : PagingRequestBase
{
    public int? CategoryId { get; set; }
}