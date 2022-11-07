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
    // [HttpGet("products")]
    public async Task<IActionResult> Index()
    {
        // var query = Request.Query[CategoryId]
        var user = User.Identity.Name;
        var request = new GetPublicProductPagingRequest()
        {
            CategoryId = Convert.ToInt32(Request.Query["CategoryId"]) != 0 ? Convert.ToInt32(Request.Query["CategoryId"]) : 1,
            PageIndex = Convert.ToInt32(Request.Query["PageIndex"]) != 0 ? Convert.ToInt32(Request.Query["PageIndex"]) : 1,
            PageSize = Convert.ToInt32(Request.Query["PageSize"]) != 0 ? Convert.ToInt32(Request.Query["PageSize"]) : 1
        };
        var ProductsByCategory = await _productApiClient.GetAllByCategoryId(request);

        return View(ProductsByCategory);
    }
    [HttpGet("products/{productId}")]
    public async Task<IActionResult> Details(int productId)
    {
        var product = await _productApiClient.GetById(productId);
        return View(product);
    }
}