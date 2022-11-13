using MidasShopSolution.Api.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using MidasShopSolution.ViewModels.Categories;


namespace MidasShopSolution.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetById(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryRequestDto request)
        {
            var categoryId = await _categoryService.Create(request);
            if (categoryId == 0)
                return BadRequest();

            var category = await _categoryService.GetById(categoryId);
            return CreatedAtAction(nameof(GetById), new { id = categoryId }, category);
        }
        // /products
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> Update(int categoryId, [FromForm] CreateCategoryRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _categoryService.Update(categoryId, request);
            if (affectedResult == 0)
                return BadRequest();


            return Ok();
        }
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var affectedResult = await _categoryService.Delete(categoryId);
            if (affectedResult == 0)
                return BadRequest();


            return Ok();
        }
    }
}