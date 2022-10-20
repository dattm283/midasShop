using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.ViewModels.Catalog.Products;

public class GetPublicProductPagingRequest: PagingRequestBase
{
    public int? CategoryId { get; set; }
}