using Microsoft.AspNetCore.Mvc;
using MidasShopSolution.Application.Catalog.Products;
using MidasShopSolution.ViewModels.Catalog.Products;
using MidasShopSolution.ViewModels.Catalog.Products.Manage;

namespace MidasShopSolution.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly IPublicProductService _publicProductService;
    private readonly IManageProductService _manageProductService;
    public ProductController(IPublicProductService publicProductService,
        IManageProductService manageProductService)
    {
        _publicProductService = publicProductService;
        _manageProductService = manageProductService;
    }

    // /product
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _publicProductService.GetAll();
        return Ok(products);
    }

    // /product/public-paging
    [HttpGet("public-paging")]
    public async Task<IActionResult> Get([FromQuery] GetPublicProductPagingRequest request)
    {
        var products = await _publicProductService.GetAllByCategoryId(request);
        return Ok(products);
    }

    // /product/:id
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(int Id, string languageId)
    {
        var product = await _manageProductService.GetById(Id, languageId);
        if (product == null)
            return BadRequest("Cannot find product");
        return Ok(product);
    }

    // /product
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
    {
        var productId = await _manageProductService.Create(request);
        if (productId == 0)
            return BadRequest();

        var product = await _manageProductService.GetById(productId, request.LanguageId);
        return CreatedAtAction(nameof(GetById), new { id = productId }, product);
    }

    // /product
    [HttpPut]
    public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
    {
        var affectedResult = await _manageProductService.Update(request);
        if (affectedResult == 0)
            return BadRequest();


        return Ok();
    }

    // /product/:Id
    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        var affectedResult = await _manageProductService.Delete(Id);
        if (affectedResult == 0)
            return BadRequest();


        return Ok();
    }

    // /product/price/:id/
    [HttpPut("price/{id}/{newPrice}")]
    public async Task<IActionResult> UpdatePrice(int id, decimal newPrice)
    {
        var isSuccessful = await _manageProductService.UpdatePrice(id, newPrice);
        if (!isSuccessful)
            return BadRequest();

        return Ok();
    }
}