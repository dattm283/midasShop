using Microsoft.AspNetCore.Mvc;
using MidasShopSolution.CustomerSite.Services;
using MidasShopSolution.ViewModels.Products;


namespace MidasShopSolution.CustomerSite.Controllers;

public class ProductsController : Controller
{
    private readonly IProductApiClient _productApiClient;
    public ProductsController(IProductApiClient productApiClient)
    {
        _productApiClient = productApiClient;
    }
    [HttpGet("products/category")]
    public async Task<IActionResult> Index()
    {
        // var query = Request.Query[CategoryId]
        var user = User.Identity.Name;
        var request = new GetPublicProductPagingRequest()
        {
            CategoryId = Convert.ToInt32(Request.Query["CategoryId"]),
            PageIndex = Convert.ToInt32(Request.Query["PageIndex"]),
            PageSize = Convert.ToInt32(Request.Query["PageSize"])
        };
        var ProductsByCategory = await _productApiClient.GetAllByCategoryId(request);

        return View(ProductsByCategory);
    }
}