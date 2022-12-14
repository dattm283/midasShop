using MidasShopSolution.ViewModels.Users;

namespace MidasShopSolution.Api.Services.Users
{
    public interface IUserService
    {
        Task<string> Authencate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
    }
}