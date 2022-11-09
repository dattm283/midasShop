using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MidasShopSolution.Api.Services.Products;
using MidasShopSolution.ViewModels.Products;
using MidasShopSolution.ViewModels.ProductImages;
using MidasShopSolution.ViewModels.Common;

namespace MidasShopSolution.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ProductsController(IProductService productService,
        IWebHostEnvironment hostingEnvironment)
    {
        _productService = productService;
        _hostingEnvironment = hostingEnvironment;
    }
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetById(int productId)
    {
        var product = await _productService.GetById(productId);
        if (product == null)
            return BadRequest("Cannot find product");
        return Ok(product);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllProduct([FromQuery]PagingRequestBase request) {
        var products = await _productService.GetAllProduct(request);
        return Ok(products);
    }
    // /products? pageIndex={pageIndex}&pageSize={pageSize}&categoryId={categoryId}
    [HttpGet("category")]
    public async Task<IActionResult> GetAllPaging([FromQuery] GetPublicProductPagingRequest request)
    {
        var products = await _productService.GetAllByCategoryId(request);
        return Ok(products);
    }
    [HttpGet("keyword")]
    public async Task<IActionResult> GetAllPagingByKeyword([FromQuery] GetManageProductPagingRequest request)
    {
        var products = await _productService.GetAllPagingByKeyword(request);
        return Ok(products);
    }
    // /products/:productId

    [HttpGet("featured/{take}")]
    public async Task<IActionResult> GetFeaturedProducts(int take)
    {
        var products = await _productService.GetFeaturedProducts(take);
        return Ok(products);
    }
    // /products
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var productId = await _productService.Create(request);
        if (productId == 0)
            return BadRequest();

        var product = await _productService.GetById(productId);
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
        var affectedResult = await _productService.Update(request);
        if (affectedResult == 0)
            return BadRequest();


        return Ok();
    }

    [HttpPatch("{productId}/{newPrice}")]
    public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
    {
        var isSuccessful = await _productService.UpdatePrice(productId, newPrice);
        if (!isSuccessful)
            return BadRequest();

        return Ok();
    }
    [HttpPut("{id}/categories")]
    public async Task<IActionResult> CategoryAssign(int id, [FromForm] CategoryAssignRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isSuccessful = await _productService.CategoryAssign(id, request);
        if (!isSuccessful)
            return BadRequest();

        return Ok();
    }
    // /products/:productId
    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
        var affectedResult = await _productService.Delete(productId);
        if (affectedResult == 0)
            return BadRequest();


        return Ok();
    }

    // /products/price/:productId/

    [HttpGet("{productId}/images")]
    public async Task<IActionResult> GetListImageByProductId(int productId)
    {
        var image = await _productService.GetListImages(productId);
        if (image == null)
            return BadRequest("Cannot find product");
        return Ok(image);
    }
    [HttpGet("{productId}/images/{imageId}")]
    public async Task<IActionResult> GetImageById(int productId, int imageId)
    {
        var image = await _productService.GetImageById(imageId);
        if (image == null)
            return BadRequest("Cannot find product");
        return Ok(image);
    }
    //Images
    [HttpPost("{productId}/images")]
    public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var imageId = await _productService.AddImage(productId, request);
        if (imageId == 0)
            return BadRequest();

        var image = await _productService.GetImageById(imageId);

        return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
    }

    [HttpPut("{productId}/images/{imageId}")]
    // [Authorize]
    public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _productService.UpdateImage(imageId, request);
        if (result == 0)
            return BadRequest();

        return Ok();
    }

    [HttpDelete("{productId}/images/{imageId}")]
    // [Authorize]
    public async Task<IActionResult> RemoveImage(int imageId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _productService.RemoveImage(imageId);
        if (result == 0)
            return BadRequest();

        return Ok();
    }


}