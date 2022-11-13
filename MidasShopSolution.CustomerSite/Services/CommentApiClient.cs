using MidasShopSolution.ViewModels.Comments;
using Newtonsoft.Json;
using System.Text;

namespace MidasShopSolution.CustomerSite.Services
{
    public class CommentApiClient : ICommentApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICommentApiClient _CommentApiClient;
        public CommentApiClient(ICommentApiClient commentApiClient, IHttpClientFactory httpClientFactory)
        {
            _CommentApiClient = commentApiClient;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<CommentDto>> GetAll()
        {
            return await _CommentApiClient.GetAll();
        }
        public async Task<List<CommentDto>> GetByProductId(int productId)
        {
            return await _CommentApiClient.GetByProductId(productId);
        }
        public async Task<CommentDto> GetById(int id)
        {
            return await _CommentApiClient.GetById(id);
        }
        public async Task<int> Create(CreateCommentRequestDto request)
        {
            return await _CommentApiClient.Create(request);
        }
        public async Task<int> Delete(int commentId)
        {
            return await _CommentApiClient.Delete(commentId);
        }
    }
}