using MidasShopSolution.Data.Enums;

namespace MidasShopSolution.Data.Entites;

public class Contact
{
    public int Id { set; get; }
    public string Name { set; get; }
    public string Email { set; get; }
    public string PhoneNumber { set; get; }
    public string Message { set; get; }
    public Status Status { set; get; }
}