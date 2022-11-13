using Refit;
using MidasShopSolution.ViewModels.Comments;

namespace MidasShopSolution.CustomerSite.Services
{
    public interface ICommentApiClient
    {
        [Get("/api/Comments")]
        Task<List<CommentDto>> GetAll();
        [Get("/api/Comments/{id}")]
        Task<CommentDto> GetById(int id);
        [Get("/api/Comments/product/{productId}")]
        Task<List<CommentDto>> GetByProductId(int productId);
        [Post("/api/Comments")]
        Task<int> Create(CreateCommentRequestDto request);
        [Delete("/api/Comments/{commentId}")]
        Task<int> Delete(int commentId);
    }
}