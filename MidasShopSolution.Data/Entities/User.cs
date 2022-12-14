using Microsoft.AspNetCore.Identity;

namespace MidasShopSolution.Data.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Dob { get; set; }

    public List<Cart> Carts { get; set; }
    public List<Order> Orders { get; set; }
    public List<Comment> Comments { get; set; }

}