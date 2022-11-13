using MidasShopSolution.ViewModels.Comments;
using MidasShopSolution.Data.EF;
using MidasShopSolution.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;
using MidasShopSolution.Data.Entities;
using MidasShopSolution.Api.Utilities.Exceptions;


namespace MidasShopSolution.Api.Services.Comments;

public class CommentService : ICommentService
{
    private readonly MidasShopDbContext _context;
    public CommentService(MidasShopDbContext context)
    {
        _context = context;
    }
    public async Task<List<CommentDto>> GetAll()
    {
        var comment = await _context.Comments.ToListAsync();
        return comment.Select(c => new CommentDto()
        {
            Id = c.Id,
            Content = c.Content,
            Rate = c.Rate,
            ProductId = c.ProductId,
            UserId = c.UserId
        }).ToList();
    }
    public async Task<List<CommentDto>> GetByProductId(int productId)
    {
        var comment = await _context.Comments.Where(c => c.ProductId == productId).ToListAsync();
        return comment.Select(c => new CommentDto()
        {
            Id = c.Id,
            Content = c.Content,
            Rate = c.Rate,
            ProductId = c.ProductId,
            UserId = c.UserId,
        }).ToList();
    }
    public async Task<CommentDto> GetById(int id)
    {
        var comment = await _context.Comments.FindAsync(id);

        return new CommentDto()
        {
            Id = comment.Id,
            Content = comment.Content,
            Rate = comment.Rate,
            ProductId = comment.ProductId,
            UserId = comment.UserId
        };
    }
    public async Task<int> Create(CreateCommentRequestDto request)
    {
        var comment = new Comment()
        {
            Content = request.Content,
            Rate = request.Rate,
            ProductId = request.ProductId,
            UserId = new Guid(request.UserId)
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return comment.Id;
    }
    public async Task<int> Update(CreateCommentRequestDto request)
    {
        throw new MidasShopException();
    }
    public async Task<int> Delete(int commentId)
    {
        var comment = await _context.Comments.FindAsync(commentId);
        if (comment == null) throw new MidasShopException($"Cannot find a comment with Id: {comment}");

        _context.Comments.Remove(comment);
        return await _context.SaveChangesAsync();
    }
}
