using MidasShopSolution.Data.Enums;

namespace MidasShopSolution.Data.Entites;

public class Order
{
    public int Id { set; get; }
    public DateTime OrderDate { set; get; }
    public Guid UserId { set; get; }
    public string ShipName { set; get; }
    public string ShipAddress { set; get; }
    public string ShipEmail { set; get; }
    public string ShipPhoneNumber { set; get; }
    public OrderStatus Status { set; get; }
    public List<OrderDetail> OrderDetails { get; set; }
}