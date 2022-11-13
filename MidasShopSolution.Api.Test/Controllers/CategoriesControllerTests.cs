using FluentAssertions;
using FakeItEasy;
using MidasShopSolution.Api.Services.Categories;
using MidasShopSolution.Api.Controllers;
using MidasShopSolution.Data;
using MidasShopSolution.ViewModels.Categories;
using Xunit;

namespace MidasShopSolution.Api.Test.Controllers;

public class CategoriesControllerTests
{
    private readonly ICategoryService _categoryService;
    public CategoriesControllerTests()
    {
        _categoryService = A.Fake<ICategoryService>();
    }
    [Fact]
    public void CategoriesController_GetAll_ReturnOk()
    {
        // arrange
        var categories = A.Fake<ICollection<CategoryDto>>();
        var controller = new CategoriesController(_categoryService);
        // act
        var result = controller.GetAll();

        // assert
        result.Should().NotBeNull();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(1031)]
    public void CategoriesController_GetById_ReturnOk(int id)
    {
        // arrange
        var category = A.Fake<CategoryDto>();
        var controller = new CategoriesController(_categoryService);
        // act
        var result = controller.GetById(id);
        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void CategoriesController_Create_ReturnOk()
    {
        var request = A.Fake<CreateCategoryRequestDto>();
        var controller = new CategoriesController(_categoryService);
        var result = controller.Create(request);
        result.Should().NotBeNull();
    }

    [Theory]
    [InlineData(1)]
    public void CategoriesController_Update_ReturnOk(int id)
    {
        var request = A.Fake<CreateCategoryRequestDto>();
        var controller = new CategoriesController(_categoryService);
        var result = controller.Update(id, request);
        result.Should().NotBeNull();
    }

    [Theory]
    [InlineData(1)]
    public void CategoriesController_Delete_ReturnOk(int id)
    {
        var request = A.Fake<CreateCategoryRequestDto>();
        var controller = new CategoriesController(_categoryService);
        var result = controller.Delete(id);
        result.Should().BeNull();
    }
}