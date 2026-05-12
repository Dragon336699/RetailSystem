using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RetailSystem.API.Controllers;
using RetailSystem.API.Contracts.Products;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.SharedLibrary.Dtos.Products;

namespace RetailSystem.Test.Controller.API
{
    public class ProductControllerTest
    {
        private readonly ProductController _controller;
        private readonly Mock<IProductService> _mockProductService;

        public ProductControllerTest()
        {
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductController(_mockProductService.Object);
        }

        // GetProducts

        [Fact]
        public async Task GetProducts_ShouldReturnProducts_WhenSuccess()
        {
            var products = new List<ProductDto>
            {
                new ProductDto { Id = Guid.NewGuid(), ProductName = "Prod1", Price = 9.99m }
            };

            _mockProductService
                .Setup(x => x.GetProductsAsync(0, 10))
                .ReturnsAsync(products);

            var result = await _controller.GetProducts();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);
            Assert.Single(model);

            _mockProductService.Verify(x => x.GetProductsAsync(0, 10), Times.Once);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            _mockProductService
                .Setup(x => x.GetProductsAsync(0, 10))
                .ReturnsAsync(new List<ProductDto>());

            var result = await _controller.GetProducts();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);
            Assert.Empty(model);
        }

        // GetProductById

        [Fact]
        public async Task GetProductById_ShouldReturnProduct_WhenFound()
        {
            var id = Guid.NewGuid();
            var expected = new ProductDto { Id = id, ProductName = "Found", Price = 1.23m };

            _mockProductService
                .Setup(x => x.GetProductByIdAsync(id))
                .ReturnsAsync(expected);

            var result = await _controller.GetProductById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal(id, model.Id);
            Assert.Equal("Found", model.ProductName);
            Assert.Equal(1.23m, model.Price);

            _mockProductService.Verify(x => x.GetProductByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            var id = Guid.NewGuid();

            _mockProductService
                .Setup(x => x.GetProductByIdAsync(id))
                .ReturnsAsync((ProductDto?)null);

            var result = await _controller.GetProductById(id);

            Assert.IsType<NotFoundResult>(result);
            _mockProductService.Verify(x => x.GetProductByIdAsync(id), Times.Once);
        }

        // GetFilteredProducts

        [Fact]
        public async Task GetFilteredProducts_ShouldReturnOk_WithProducts()
        {
            var categoryId = Guid.NewGuid();

            var fakeProducts = new List<ProductDto>
            {
                new ProductDto { Id = Guid.NewGuid(), ProductName = "Product A", Price = 10m },
                new ProductDto { Id = Guid.NewGuid(), ProductName = "Product B", Price = 20m }
            };

            _mockProductService
                .Setup(x => x.GetFilteredProducts(categoryId, 0, 10))
                .ReturnsAsync(fakeProducts);

            var result = await _controller.GetFilteredProducts(categoryId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());

            _mockProductService.Verify(x => x.GetFilteredProducts(categoryId, 0, 10), Times.Once);
        }

        [Fact]
        public async Task GetFilteredProducts_ShouldReturnOk_WithEmptyList_WhenNoMatch()
        {
            var categoryId = Guid.NewGuid();

            _mockProductService
                .Setup(x => x.GetFilteredProducts(categoryId, 0, 10))
                .ReturnsAsync(new List<ProductDto>());

            var result = await _controller.GetFilteredProducts(categoryId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);
            Assert.Empty(returnValue);
        }

        // AddProduct

        [Fact]
        public async Task AddProduct_ShouldReturnOk_WhenSuccess()
        {
            var request = new CreateProductRequest
            {
                ProductName = "New Prod",
                Price = 5.00m,
                CategoryIds = new List<Guid> { Guid.NewGuid() },
                ProductImages = new List<IFormFile>(),
                ThumbnailIndex = 0,
                SizesQuantity = new List<UploadProductSizeDto>
                {
                    new UploadProductSizeDto { SizeId = Guid.NewGuid(), Quantity = 1 }
                }
            };

            var expected = new ProductDto { Id = Guid.NewGuid(), ProductName = "New Prod", Price = 5.00m };

            _mockProductService
                .Setup(x => x.AddProductAsync(It.IsAny<CreateProductCommand>()))
                .ReturnsAsync(expected);

            var result = await _controller.AddProduct(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal(expected.Id, model.Id);
            Assert.Equal("New Prod", model.ProductName);
            Assert.Equal(5.00m, model.Price);

            _mockProductService.Verify(x => x.AddProductAsync(It.IsAny<CreateProductCommand>()), Times.Once);
        }

        [Fact]
        public async Task AddProduct_ShouldThrowException_WhenServiceFails()
        {
            var request = new CreateProductRequest
            {
                ProductName = "Fail Prod",
                Price = 0m,
                CategoryIds = new List<Guid>(),
                ProductImages = new List<IFormFile>(),
                ThumbnailIndex = 0,
                SizesQuantity = new List<UploadProductSizeDto>()
            };

            _mockProductService
                .Setup(x => x.AddProductAsync(It.IsAny<CreateProductCommand>()))
                .ThrowsAsync(new Exception("Failed to upload product images."));

            await Assert.ThrowsAsync<Exception>(() =>
                _controller.AddProduct(request));
        }

        // UpdateProduct

        [Fact]
        public async Task UpdateProduct_ShouldReturnOk_WhenSuccess()
        {
            var request = new UpdateProductRequest
            {
                Id = Guid.NewGuid(),
                ProductName = "Updated",
                Price = 7.77m,
                CategoryIds = new List<Guid> { Guid.NewGuid() },
                ProductImages = new List<IFormFile>(),
                SizesQuantity = new List<UploadProductSizeDto>
                {
                    new UploadProductSizeDto { SizeId = Guid.NewGuid(), Quantity = 2 }
                }
            };

            var expected = new ProductDto { Id = request.Id, ProductName = "Updated", Price = 7.77m };

            _mockProductService
                .Setup(x => x.UpdateProductAsync(It.IsAny<UpdateProductCommand>()))
                .ReturnsAsync(expected);

            var result = await _controller.UpdateProduct(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal(expected.Id, model.Id);
            Assert.Equal("Updated", model.ProductName);
            Assert.Equal(7.77m, model.Price);

            _mockProductService.Verify(x => x.UpdateProductAsync(It.IsAny<UpdateProductCommand>()), Times.Once);
        }

        [Fact]
        public async Task UpdateProduct_ShouldThrowKeyNotFoundException_WhenProductDoesNotExist()
        {
            var request = new UpdateProductRequest
            {
                Id = Guid.NewGuid(),
                ProductName = "Ghost",
                Price = 1.00m,
                CategoryIds = new List<Guid>(),
                ProductImages = new List<IFormFile>(),
                SizesQuantity = new List<UploadProductSizeDto>()
            };

            _mockProductService
                .Setup(x => x.UpdateProductAsync(It.IsAny<UpdateProductCommand>()))
                .ThrowsAsync(new KeyNotFoundException("Product not found."));

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _controller.UpdateProduct(request));
        }

        // DeleteProduct

        [Fact]
        public async Task DeleteProduct_ShouldReturnOk_WhenSuccess()
        {
            var id = Guid.NewGuid();

            _mockProductService
                .Setup(x => x.DeleteProductAsync(id))
                .Returns(Task.CompletedTask);

            var result = await _controller.DeleteProduct(id);

            Assert.IsType<OkResult>(result);
            _mockProductService.Verify(x => x.DeleteProductAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            var id = Guid.NewGuid();

            _mockProductService
                .Setup(x => x.DeleteProductAsync(id))
                .ThrowsAsync(new KeyNotFoundException($"Product {id} not found"));

            // Adjust assertion type to match your controller's actual error handling
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _controller.DeleteProduct(id));
        }
    }
}