using MidasShopSolution.ViewModels.System.Users;

namespace MidasShopSolution.Api.Application.System
{
    public interface IUserService
    {
        Task<string> Authencate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
    }
}