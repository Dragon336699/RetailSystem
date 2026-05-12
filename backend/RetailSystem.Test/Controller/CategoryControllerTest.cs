using RetailSystem.API.Controllers;
using Moq;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.SharedLibrary.Dtos.Categories;
using Microsoft.AspNetCore.Mvc;

namespace RetailSystem.Test.Controller
{
    public class CategoryControllerTest
    {
        private readonly CategoryController _controller;
        private readonly Mock<ICategoryService> _mockCategoryService;
        public CategoryControllerTest()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _controller = new CategoryController(_mockCategoryService.Object);
        }

        //GET CATEGORIES
        [Fact]
        public async Task GetAll_ShouldReturnCategories_WhenSuccess()
        {
            //Arrange
            var categories = new List<CategoryDto>{new CategoryDto{
                Id = Guid.Parse("324BAA91-7215-4AA5-DC74-08DEAF8A0B5D"),
                CategoryName = "Running"
            }};

            _mockCategoryService.Setup(x => x.GetAllCategoriesAsync()).ReturnsAsync(categories);

            //Act
            var result = await _controller.GetAllCategories();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<CategoryDto>>(okResult.Value);

            Assert.Single(model);
            Assert.Equal("Running", model.First().CategoryName);
            Assert.Equal(Guid.Parse("324BAA91-7215-4AA5-DC74-08DEAF8A0B5D"), model.First().Id);
        }

        [Fact]
        public async Task GetAllCategories_ShouldReturnEmptyList_WhenNoData()
        {
            // Arrange
            var categories = new List<CategoryDto>();

            _mockCategoryService
                .Setup(x => x.GetAllCategoriesAsync())
                .ReturnsAsync(categories);

            // Act
            var result = await _controller.GetAllCategories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<CategoryDto>>(okResult.Value);

            Assert.Empty(model);
        }

        [Fact]
        public async Task AddNewCategories_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var request = new CreateCategoryCommand
            {
                CategoryName = "Running",
                Description = "Sports category"
            };

            var expectedResult = new CategoryDto
            {
                Id = Guid.NewGuid(),
                CategoryName = "Running"
            };

            _mockCategoryService
                .Setup(x => x.AddCategoryAsync(request))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.AddNewCategories(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var model = Assert.IsType<CategoryDto>(okResult.Value);

            Assert.Equal("Running", model.CategoryName);
            Assert.Equal(expectedResult.Id, model.Id);

            _mockCategoryService.Verify(x => x.AddCategoryAsync(request), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoy_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var request = new UpdateCategoryCommand
            {
                Id = Guid.NewGuid(),
                CategoryName = "Running Updated",
                Description = "Updated description"
            };

            var expectedResult = new CategoryDto
            {
                Id = request.Id,
                CategoryName = "Running Updated"
            };

            _mockCategoryService
                .Setup(x => x.UpdateCategoryAsync(request))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.UpdateCategoy(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var model = Assert.IsType<CategoryDto>(okResult.Value);

            Assert.Equal("Running Updated", model.CategoryName);
            Assert.Equal(expectedResult.Id, model.Id);
        }

        [Fact]
        public async Task DeleteCategory_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockCategoryService
                .Setup(x => x.DeleteCategoryAsync(id))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCategory(id);

            // Assert
            Assert.IsType<OkResult>(result);

            _mockCategoryService.Verify(x => x.DeleteCategoryAsync(id), Times.Once);
        }


    }
}
