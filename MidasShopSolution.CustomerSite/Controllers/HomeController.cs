using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MidasShopSolution.CustomerSite.Models;
using MidasShopSolution.CustomerSite.Services;
using Microsoft.AspNetCore.Authorization;
using MidasShopSolution.ViewModels.Products;


namespace MidasShopSolution.CustomerSite.Controllers;

// [Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductApiClient _productApiClient;

    public HomeController(ILogger<HomeController> logger,
    IProductApiClient productApiClient)
    {
        _logger = logger;
        _productApiClient = productApiClient;
    }

    public async Task<IActionResult> Index()
    {
        var user = User.Identity.Name;
        var FeaturedProducts = await _productApiClient.GetFeaturedProducts(3);

        return View(FeaturedProducts);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}