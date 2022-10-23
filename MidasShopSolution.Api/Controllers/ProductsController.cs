using Microsoft.AspNetCore.Mvc;
using MidasShopSolution.Application.Catalog.Products;
using MidasShopSolution.ViewModels.Catalog.Products;

namespace MidasShopSolution.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IPublicProductService _publicProductService;
    private readonly IManageProductService _manageProductService;
    public ProductsController(IPublicProductService publicProductService,
        IManageProductService manageProductService)
    {
        _publicProductService = publicProductService;
        _manageProductService = manageProductService;
    }

    // /products? pageIndex={pageIndex}&pageSize={pageSize}&categoryId={categoryId}
    [HttpGet]
    public async Task<IActionResult> GetAllPaging([FromQuery] GetPublicProductPagingRequest request)
    {
        var products = await _publicProductService.GetAllByCategoryId(request);
        return Ok(products);
    }

    // /products/:productId
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetById(int productId)
    {
        var product = await _manageProductService.GetById(productId);
        if (product == null)
            return BadRequest("Cannot find product");
        return Ok(product);
    }

    // /products
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var productId = await _manageProductService.Create(request);
        if (productId == 0)
            return BadRequest();

        var product = await _manageProductService.GetById(productId);
        return CreatedAtAction(nameof(GetById), new { id = productId }, product);
    }

    // /products
    [HttpPut]
    public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var affectedResult = await _manageProductService.Update(request);
        if (affectedResult == 0)
            return BadRequest();


        return Ok();
    }

    // /products/:productId
    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
        var affectedResult = await _manageProductService.Delete(productId);
        if (affectedResult == 0)
            return BadRequest();


        return Ok();
    }

    // /products/price/:productId/
    [HttpPatch("{productId}/{newPrice}")]
    public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
    {
        var isSuccessful = await _manageProductService.UpdatePrice(productId, newPrice);
        if (!isSuccessful)
            return BadRequest();

        return Ok();
    }
}