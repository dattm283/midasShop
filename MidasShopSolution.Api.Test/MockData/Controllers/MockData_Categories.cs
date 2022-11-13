using MidasShopSolution.ViewModels.Categories;
using MidasShopSolution.Data.Entities;

namespace MidasShopSolution.Api.Test.MockData.Controllers
{
    public class MockData_Categories
    {
        public static List<CategoryDto> GetAllCategory()
        {
            return new List<CategoryDto>
                {
                    new CategoryDto
                    {
                        Id= 1,
                        Name= "Sach",
                        ParentId= null
                    },
                    new CategoryDto
                    {
                        Id= 2,
                        Name= "Viet",
                        ParentId= null
                    },
                    new CategoryDto
                    {
                        Id = 4,
                        Name = "Thuoc",
                        ParentId = null
                    }
                };
        }
    }
}