using Microsoft.AspNetCore.Mvc;
using MidasShopSolution.CustomerSite.Services;
using MidasShopSolution.ViewModels.Products;
using System.Security.Claims;

namespace MidasShopSolution.CustomerSite.Controllers;

public class ProductsController : Controller
{
    private readonly IProductApiClient _productApiClient;
    private readonly ICategoryApiClient _categoryApiClient;
    private readonly ICommentApiClient _commentApiClient;

    // private readonly ICommentApiClient _commentApiClient;
    public ProductsController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient, ICommentApiClient commentApiClient)
    {
        _productApiClient = productApiClient;
        _categoryApiClient = categoryApiClient;
        _commentApiClient = commentApiClient;
    }
    [HttpGet("products")]
    public async Task<IActionResult> Index()
    {
        // var query = Request.Query[CategoryId]
        var user = User.Identity.Name;
        ViewData["CategoryList"] = await _categoryApiClient.GetAll();
        var requestByCategoryId = new GetPublicProductPagingRequest()
        {
            CategoryId = Convert.ToInt32(Request.Query["CategoryId"]) != 0 ? Convert.ToInt32(Request.Query["CategoryId"]) : 1,
            PageIndex = Convert.ToInt32(Request.Query["PageIndex"]) != 0 ? Convert.ToInt32(Request.Query["PageIndex"]) : 1,
            PageSize = Convert.ToInt32(Request.Query["PageSize"]) != 0 ? Convert.ToInt32(Request.Query["PageSize"]) : 1
        };
        ViewData["CategoryId"] = requestByCategoryId.CategoryId;
        ViewData["PageIndex"] = requestByCategoryId.PageIndex;
        ViewData["PageSize"] = requestByCategoryId.PageSize;


        var ProductsByCategory = await _productApiClient.GetAllByCategoryId(requestByCategoryId);
        return View(ProductsByCategory);
    }
    [HttpGet("products/{productId}")]
    public async Task<IActionResult> Details(int productId)
    {
        var product = await _productApiClient.GetById(productId);
        var userId = User.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value;
        ViewBag.userId = userId;
        ViewData["CategoryList"] = await _categoryApiClient.GetAll();
        ViewData["CommentList"] = await _commentApiClient.GetByProductId(productId);
        return View(product);

    }
}