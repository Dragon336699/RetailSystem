using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RetailSystem.SharedLibrary.Dtos.Categories;
using RetailSystem.SharedLibrary.Dtos.Products;
using RetailSystem.UI.Controllers;
using RetailSystem.UI.Models.Home;
using RetailSystem.UI.Services.Intefaces;
using Xunit;

namespace RetailSystem.Test.Controller.MVC
{
    public class HomeControllerTest
    {
        private readonly HomeController _controller;
        private readonly Mock<IProductApiClient> _mockProductApi;
        private readonly Mock<ICategoryApiClient> _mockCategoryApi;
        private readonly Mock<ILogger<HomeController>> _mockLogger;

        public HomeControllerTest()
        {
            _mockProductApi = new Mock<IProductApiClient>();
            _mockCategoryApi = new Mock<ICategoryApiClient>();
            _mockLogger = new Mock<ILogger<HomeController>>();

            _controller = new HomeController(_mockLogger.Object, _mockProductApi.Object, _mockCategoryApi.Object);
        }

        [Fact]
        public async Task Index_ShouldReturnViewWithHomeViewModel_WhenSuccess()
        {
            // Arrange
            var products = new List<ProductDto>
            {
                new ProductDto
                {
                    Id = Guid.NewGuid(),
                    ProductName = "ProdA",
                    Price = 12.34m,
                    ProductImages = new List<ProductImageDto>(),
                    Categories = new List<CategoryDto>(),
                    ProductVariants = new List<ProductVariantDto>(),
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                }
            };

            var categories = new List<CategoryDto>
            {
                new CategoryDto { Id = Guid.NewGuid(), CategoryName = "CatA" }
            };

            _mockProductApi.Setup(x => x.GetFeatureProducts()).ReturnsAsync(products);
            _mockCategoryApi.Setup(x => x.GetAllCategoriesAsync()).ReturnsAsync(categories);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<HomeViewModel>(viewResult.Model);
            Assert.Single(model.FeatureProducts);
            Assert.Single(model.Categories);
            Assert.Equal("ProdA", model.FeatureProducts[0].ProductName);

            _mockProductApi.Verify(x => x.GetFeatureProducts(), Times.Once);
            _mockCategoryApi.Verify(x => x.GetAllCategoriesAsync(), Times.Once);
        }

        [Fact]
        public async Task Index_ShouldReturnViewWithEmptyCollections_WhenApisReturnEmpty()
        {
            // Arrange
            _mockProductApi.Setup(x => x.GetFeatureProducts()).ReturnsAsync(new List<ProductDto>());
            _mockCategoryApi.Setup(x => x.GetAllCategoriesAsync()).ReturnsAsync(new List<CategoryDto>());

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<HomeViewModel>(viewResult.Model);
            Assert.Empty(model.FeatureProducts);
            Assert.Empty(model.Categories);
        }
    }
}
