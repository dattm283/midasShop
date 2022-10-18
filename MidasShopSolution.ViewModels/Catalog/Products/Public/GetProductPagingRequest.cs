using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.ViewModels.Catalog.Products.Public;

public class GetProductPagingRequest: PagingRequestBase
{
    public int? CategoryId { get; set; }
}