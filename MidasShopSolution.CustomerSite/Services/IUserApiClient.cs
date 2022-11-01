using MidasShopSolution.ViewModels.Users;

namespace MidasShopSolution.CustomerSite.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);
    }
}