using Microsoft.AspNetCore.Identity;

namespace MidasShopSolution.Data.Entities;

public class Role : IdentityRole<Guid>
{
    public string Description { get; set; }
}