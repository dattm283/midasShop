using MidasShopSolution.ViewModels.Slides;

namespace MidasShopSolution.Api.ApiIntegration
{
    public interface ISlideApiClient
    {
        Task<List<SlideVm>> GetAll();
    }
}