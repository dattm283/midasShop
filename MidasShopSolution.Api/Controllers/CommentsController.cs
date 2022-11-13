using MidasShopSolution.Api.Services.Comments;
using MidasShopSolution.Api.Services.Products;
using Microsoft.AspNetCore.Mvc;
using MidasShopSolution.ViewModels.Comments;

namespace MidasShopSolution.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IProductService _productService;
        public CommentsController(ICommentService commentService, IProductService productService)
        {
            _commentService = commentService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentService.GetAll();
            return Ok(comments);
        }
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var comments = await _commentService.GetByProductId(productId);
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentService.GetById(id);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentRequestDto request)
        {
            var commentId = await _commentService.Create(request);
            if (commentId == 0)
                return BadRequest();

            await _productService.UpdateStar(request.ProductId);
            var comment = await _commentService.GetById(commentId);
            return Ok(comment);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> Delete(int commentId)
        {
            var comment = await _commentService.GetById(commentId);
            var productId = comment.ProductId;
            var affectedResult = await _commentService.Delete(commentId);
            if (affectedResult == 0)
                return BadRequest();
            await _productService.UpdateStar(productId);
            return Ok();
        }
    }
}