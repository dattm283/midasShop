
namespace MidasShopSolution.ViewModels.Comments
{
    public class CreateCommentRequestDto
    {
        public string Content { get; set; } = String.Empty;
        public int Rate { get; set; } = 5;
        public int ProductId { get; set; }
        public string UserId { get; set; }
    }
}