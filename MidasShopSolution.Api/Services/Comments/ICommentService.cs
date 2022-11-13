using MidasShopSolution.ViewModels.Comments;

namespace MidasShopSolution.Api.Services.Comments
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAll();
        Task<List<CommentDto>> GetByProductId(int productId);
        Task<CommentDto> GetById(int id);
        Task<int> Create(CreateCommentRequestDto request);
        Task<int> Update(CreateCommentRequestDto request);
        Task<int> Delete(int commentId);
    }
}