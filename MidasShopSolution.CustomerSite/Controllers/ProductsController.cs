using Microsoft.AspNetCore.Mvc;
using MidasShopSolution.CustomerSite.Services;

namespace MidasShopSolution.CustomerSite.Controllers;

public class ProductsController : Controller
{
    private readonly IProductApiClient _productApiClient;
    private readonly IConfiguration _configuration;
    public ProductsController(IProductApiClient productApiClient, IConfiguration configuration)
    {
        _productApiClient = productApiClient;
        _configuration = configuration;
    }
    public IActionResult Index()
    {
        return View();
    }
}