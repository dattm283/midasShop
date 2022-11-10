namespace MidasShopSolution.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = String.Empty;
        public int Rate { get; set; } = 5;
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Product Product { get; set; }
        public User User { get; set; }
    }
}