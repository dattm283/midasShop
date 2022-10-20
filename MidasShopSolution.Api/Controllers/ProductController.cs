using Microsoft.AspNetCore.Mvc;
using MidasShopSolution.Application.Catalog.Products;

namespace MidasShopSolution.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly IPublicProductService _publicProductService;
    public ProductController(IPublicProductService publicProductService)
    {
        _publicProductService = publicProductService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _publicProductService.GetAll();
        return Ok(products);
    }
}