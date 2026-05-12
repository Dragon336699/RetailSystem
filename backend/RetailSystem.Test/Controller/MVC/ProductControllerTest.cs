using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RetailSystem.SharedLibrary.Dtos.Categories;
using RetailSystem.SharedLibrary.Dtos.Products;
using RetailSystem.SharedLibrary.Dtos.Sizes;
using RetailSystem.UI.Controllers;
using RetailSystem.UI.Models.Product;
using RetailSystem.UI.Services.Intefaces;
using Xunit;

namespace RetailSystem.Test.Controller.MVC
{
    public class ProductControllerTest
    {
        private readonly ProductController _controller;
        private readonly Mock<IProductApiClient> _mockProductApi;
        private readonly Mock<ICategoryApiClient> _mockCategoryApi;
        private readonly Mock<ISizeApiClient> _mockSizeApi;

        public ProductControllerTest()
        {
            _mockProductApi = new Mock<IProductApiClient>();
            _mockCategoryApi = new Mock<ICategoryApiClient>();
            _mockSizeApi = new Mock<ISizeApiClient>();

            _controller = new ProductController(_mockProductApi.Object, _mockCategoryApi.Object, _mockSizeApi.Object);
        }

        [Fact]
        public async Task Index_ShouldReturnViewWithAllProductViewModel()
        {
            var categories = new List<CategoryDto> { new CategoryDto { Id = Guid.NewGuid(), CategoryName = "C1" } };
            var products = new List<ProductDto> { new ProductDto { Id = Guid.NewGuid(), ProductName = "P1", ProductImages = new List<ProductImageDto>(), Categories = new List<CategoryDto>(), ProductVariants = new List<ProductVariantDto>(), CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow, Price = 1m } };

            _mockCategoryApi.Setup(x => x.GetAllCategoriesAsync()).ReturnsAsync(categories);
            _mockProductApi.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);

            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<AllProductViewModel>(viewResult.Model);
            Assert.Single(model.Categories);
            Assert.Single(model.Products);
            Assert.Equal("P1", model.Products[0].ProductName);
        }

        [Fact]
        public async Task Detail_ShouldReturnViewWithProductViewModel()
        {
            var id = Guid.NewGuid();
            var product = new ProductDto { Id = id, ProductName = "PDetail", ProductImages = new List<ProductImageDto>(), Categories = new List<CategoryDto>(), ProductVariants = new List<ProductVariantDto>(), CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow, Price = 2m };
            var sizes = new List<SizeDto> { new SizeDto { Id = Guid.NewGuid(), SizeNumber = 40.5m } };

            _mockProductApi.Setup(x => x.GetProductByIdAsync(id)).ReturnsAsync(product);
            _mockSizeApi.Setup(x => x.GetSizesAsync()).ReturnsAsync(sizes);

            var result = await _controller.Detail(id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ProductViewModel>(viewResult.Model);
            Assert.Equal("PDetail", model.Product.ProductName);
            Assert.Single(model.Sizes);
        }
    }
}

