using Microsoft.AspNetCore.Identity;

namespace MidasShopSolution.Data.Entites;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Dob { get; set; }

    public List<Cart> Carts { get; set; }
    public List<Order> Orders { get; set; }
}