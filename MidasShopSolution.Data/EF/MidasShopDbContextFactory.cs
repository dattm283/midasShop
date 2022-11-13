using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MidasShopSolution.Data.EF;

public class MidasShopDbContextFactory : IDesignTimeDbContextFactory<MidasShopDbContext>
{
    public MidasShopDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("MidasShopSolutionDb");

        var optionsBuilder = new DbContextOptionsBuilder<MidasShopDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new MidasShopDbContext(optionsBuilder.Options);
    }
}