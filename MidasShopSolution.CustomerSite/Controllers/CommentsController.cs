using MidasShopSolution.CustomerSite.Services;
using Microsoft.AspNetCore.Mvc;
using MidasShopSolution.ViewModels.Comments;

namespace MidasShopSolution.CustomerSite.Controllers
{
    public class CommentsController : Controller
    {
        // private readonly ILogger<CommentsController> _logger;
        private readonly ICommentApiClient _commentApiClient;

        public CommentsController(ICommentApiClient commentApiClient)
        {
            _commentApiClient = commentApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("comments")]
        public async Task<IActionResult> Create(CreateCommentRequestDto request)
        {
            try
            {
                await _commentApiClient.Create(request);
            }
            catch
            {
                return RedirectToAction("Details", "Products", new { productId = request.ProductId });
            }

            return RedirectToAction("Details", "Products", new { productId = request.ProductId });
        }

        [HttpPost("comments/delete/{commentId}")]
        public async Task<IActionResult> Delete(int commentId)
        {
            var comment = await _commentApiClient.GetById(commentId);
            try
            {
                await _commentApiClient.Delete(commentId);
            }
            catch
            {
                return RedirectToAction("Details", "Products", new { productId = comment.ProductId });
            }
            // return RedirectToAction("Index", "Products");
            return RedirectToAction("Details", "Products", new { productId = comment.ProductId });

        }
    }
}