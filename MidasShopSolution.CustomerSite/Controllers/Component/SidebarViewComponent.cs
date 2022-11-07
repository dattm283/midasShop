using Microsoft.AspNetCore.Mvc;
using MidasShopSolution.CustomerSite.Services;
using MidasShopSolution.ViewModels.Categories;



namespace MidasShopSolution.CustomerSite.Controllers.Component
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;
        public SidebarViewComponent(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _categoryApiClient.GetAll();
            return View(items);
        }
    }
}