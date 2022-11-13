using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidasShopSolution.ViewModels.Comments
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = String.Empty;
        public int Rate { get; set; } = 5;
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}