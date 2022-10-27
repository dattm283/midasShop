namespace MidasShopSolution.Data.Entities;

public class Cart
{
    public int Id { set; get; }
    public int ProductId { set; get; }
    public int Quantity { set; get; }
    public decimal Price { set; get; }

    public Guid UserId { get; set; }
    public Product Product { get; set; }
    public DateTime DateCreated { get; set; }
    public User User { get; set; }
}