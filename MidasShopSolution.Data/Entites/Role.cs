using Microsoft.AspNetCore.Identity;

namespace MidasShopSolution.Data.Entites;

public class Role: IdentityRole<Guid>
{
    public string Description { get; set; }
}