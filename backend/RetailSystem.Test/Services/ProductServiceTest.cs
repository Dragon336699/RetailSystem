using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.Application.Interfaces.UnitOfWork;
using RetailSystem.Application.Interfaces.Repositories;
using RetailSystem.Application.Services;
using RetailSystem.Domain.Entities;
using RetailSystem.SharedLibrary.Dtos.ImagesUpload;
using RetailSystem.SharedLibrary.Dtos.Products;
using Xunit;

namespace RetailSystem.Test.Services
{
    public class ProductServiceTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly Mock<ICategoryRepository> _mockCategoryRepo;
        private readonly Mock<IProductImageRepository> _mockProductImageRepo;
        private readonly Mock<IProductVariantRepository> _mockProductVariantRepo;
        private readonly Mock<ICloudinaryService> _mockCloudinary;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IServiceProvider> _mockServiceProvider;
        private readonly ProductService _service;

        public ProductServiceTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepo = new Mock<IProductRepository>();
            _mockCategoryRepo = new Mock<ICategoryRepository>();
            _mockProductImageRepo = new Mock<IProductImageRepository>();
            _mockProductVariantRepo = new Mock<IProductVariantRepository>();
            _mockCloudinary = new Mock<ICloudinaryService>();
            _mockMapper = new Mock<IMapper>();
            _mockServiceProvider = new Mock<IServiceProvider>();

            _mockUnitOfWork.SetupGet(u => u.Products).Returns(_mockProductRepo.Object);
            _mockUnitOfWork.SetupGet(u => u.Categories).Returns(_mockCategoryRepo.Object);
            _mockUnitOfWork.SetupGet(u => u.ProductImages).Returns(_mockProductImageRepo.Object);
            _mockUnitOfWork.SetupGet(u => u.ProductVariants).Returns(_mockProductVariantRepo.Object);

            // By default no validator registered
            _mockServiceProvider.Setup(sp => sp.GetService(It.IsAny<Type>())).Returns(null);

            _service = new ProductService(_mockUnitOfWork.Object, _mockServiceProvider.Object, _mockMapper.Object, _mockCloudinary.Object);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsNull_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _mockProductRepo.Setup(r => r.GetWithConditionAndIncludeAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Product, bool>>>(), It.IsAny<System.Linq.Expressions.Expression<Func<Product, object>>[]>() ))
                .ReturnsAsync((Product?)null);

            var result = await _service.GetProductByIdAsync(id);

            Assert.Null(result);
            _mockProductRepo.Verify(r => r.GetWithConditionAndIncludeAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Product, bool>>>(), It.IsAny<System.Linq.Expressions.Expression<Func<Product, object>>[]>()), Times.Once);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsMappedDto_WhenFound()
        {
            var id = Guid.NewGuid();
            var product = new Product { Id = id, ProductName = "P1", Price = 2.5m, ProductImages = new List<ProductImage>(), Categories = new List<Category>(), ProductVariants = new List<ProductVariant>() };

            var dto = new ProductDto { Id = id, ProductName = "P1", Price = 2.5m, ProductImages = new List<SharedLibrary.Dtos.Products.ProductImageDto>(), Categories = new List<SharedLibrary.Dtos.Categories.CategoryDto>(), ProductVariants = new List<SharedLibrary.Dtos.Products.ProductVariantDto>(), CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow };

            _mockProductRepo.Setup(r => r.GetWithConditionAndIncludeAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Product, bool>>>(), It.IsAny<System.Linq.Expressions.Expression<Func<Product, object>>[]>() ))
                .ReturnsAsync(product);

            _mockMapper.Setup(m => m.Map<ProductDto>(product)).Returns(dto);

            var result = await _service.GetProductByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(dto.Id, result!.Id);
            _mockMapper.Verify(m => m.Map<ProductDto>(product), Times.Once);
        }

        [Fact]
        public async Task AddProductAsync_ShouldAddAndReturnDto_WhenUploadSucceeds()
        {
            var cmd = new CreateProductCommand
            {
                ProductName = "New",
                Price = 10m,
                CategoryIds = new List<Guid> { Guid.NewGuid() },
                ProductImages = new List<ImageUploadDto> { new ImageUploadDto { FileName = "f", Content = new System.IO.MemoryStream(), ContentType = "image/png" } },
                ThumbnailIndex = 0,
                SizesQuantity = new List<UploadProductSizeDto> { new UploadProductSizeDto { SizeId = Guid.NewGuid(), Quantity = 1 } }
            };

            var uploadedUrls = new List<string> { "http://img/1.png" };

            _mockCloudinary.Setup(c => c.UploadImages(cmd.ProductImages, "products")).ReturnsAsync(uploadedUrls);

            _mockCategoryRepo.Setup(c => c.GetByIdsAsync(cmd.CategoryIds)).ReturnsAsync(new List<Category> { new Category { Id = cmd.CategoryIds.First(), CategoryName = "c" } });

            _mockProductRepo.Setup(r => r.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var expectedDto = new ProductDto { Id = Guid.NewGuid(), ProductName = "New", Price = 10m, ProductImages = new List<SharedLibrary.Dtos.Products.ProductImageDto>(), Categories = new List<SharedLibrary.Dtos.Categories.CategoryDto>(), ProductVariants = new List<SharedLibrary.Dtos.Products.ProductVariantDto>(), CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow };

            _mockMapper.Setup(m => m.Map<ProductDto>(It.IsAny<Product>())).Returns(expectedDto);

            var result = await _service.AddProductAsync(cmd);

            Assert.Equal(expectedDto.Id, result.Id);
            _mockCloudinary.Verify(c => c.UploadImages(cmd.ProductImages, "products"), Times.Once);
            _mockProductRepo.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
            _mockMapper.Verify(m => m.Map<ProductDto>(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task AddProductAsync_ShouldThrow_WhenUploadFails()
        {
            var cmd = new CreateProductCommand
            {
                ProductName = "New",
                Price = 10m,
                CategoryIds = new List<Guid> { Guid.NewGuid() },
                ProductImages = new List<ImageUploadDto>(),
                ThumbnailIndex = 0,
                SizesQuantity = new List<UploadProductSizeDto>()
            };

            _mockCloudinary.Setup(c => c.UploadImages(It.IsAny<List<ImageUploadDto>>(), "products")).ReturnsAsync((List<string>?)null);

            await Assert.ThrowsAsync<Exception>(() => _service.AddProductAsync(cmd));

            _mockCloudinary.Verify(c => c.UploadImages(It.IsAny<List<ImageUploadDto>>(), "products"), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldThrow_WhenProductNotFound()
        {
            var cmd = new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                ProductName = "X",
                Price = 1m,
                CategoryIds = new List<Guid>(),
                ProductImages = new List<ImageUploadDto>(),
                SizesQuantity = new List<UploadProductSizeDto>()
            };

            _mockCategoryRepo.Setup(c => c.GetByIdsAsync(cmd.CategoryIds)).ReturnsAsync(new List<Category>());
            _mockProductRepo.Setup(p => p.GetWithConditionAndIncludeAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Product, bool>>>(), It.IsAny<System.Linq.Expressions.Expression<Func<Product, object>>[]>() )).ReturnsAsync((Product?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateProductAsync(cmd));
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateVariants_RemoveImages_SetThumbnail_AddNewImages()
        {
            // Arrange existing product with one variant and two images
            var existingVariant = new ProductVariant { Id = Guid.NewGuid(), StockQuantity = 5, SizeId = Guid.NewGuid() };
            var img1 = new ProductImage { Id = Guid.NewGuid(), ImageUrl = "a.png", IsThumbnail = true };
            var img2 = new ProductImage { Id = Guid.NewGuid(), ImageUrl = "b.png", IsThumbnail = false };

            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Old",
                Price = 1m,
                ProductVariants = new List<ProductVariant> { existingVariant },
                ProductImages = new List<ProductImage> { img1, img2 },
                Categories = new List<Category>()
            };

            var cmd = new UpdateProductCommand
            {
                Id = product.Id,
                ProductName = "Updated",
                Price = 9.9m,
                CategoryIds = new List<Guid>(),
                ProductImages = new List<ImageUploadDto> { new ImageUploadDto { FileName = "n", Content = new System.IO.MemoryStream(), ContentType = "image/png" } },
                RemoveImageIds = new List<Guid> { img2.Id },
                ThumbnailImageId = img1.Id,
                ThumbnailIndex = 0,
                SizesQuantity = new List<UploadProductSizeDto>
                {
                    new UploadProductSizeDto { Id = existingVariant.Id, SizeId = existingVariant.SizeId, Quantity = 7 },
                    new UploadProductSizeDto { Id = null, SizeId = Guid.NewGuid(), Quantity = 3 }
                }
            };

            _mockCategoryRepo.Setup(c => c.GetByIdsAsync(cmd.CategoryIds)).ReturnsAsync(new List<Category>());
            _mockProductRepo.Setup(p => p.GetWithConditionAndIncludeAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Product, bool>>>(), It.IsAny<System.Linq.Expressions.Expression<Func<Product, object>>[]>() )).ReturnsAsync(product);
            _mockCloudinary.Setup(c => c.UploadImages(cmd.ProductImages, "products")).ReturnsAsync(new List<string> { "new1.png" });

            _mockProductVariantRepo.Setup(v => v.Add(It.IsAny<ProductVariant>()));
            _mockProductImageRepo.Setup(pi => pi.RemoveRange(It.IsAny<IEnumerable<ProductImage>>()));
            _mockProductImageRepo.Setup(pi => pi.AddRange(It.IsAny<IEnumerable<ProductImage>>()));
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            var expectedDto = new ProductDto { Id = product.Id, ProductName = "Updated", Price = 9.9m, ProductImages = new List<SharedLibrary.Dtos.Products.ProductImageDto>(), Categories = new List<SharedLibrary.Dtos.Categories.CategoryDto>(), ProductVariants = new List<SharedLibrary.Dtos.Products.ProductVariantDto>(), CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow };
            _mockMapper.Setup(m => m.Map<ProductDto>(product)).Returns(expectedDto);

            // Act
            var result = await _service.UpdateProductAsync(cmd);

            // Assert
            Assert.Equal(expectedDto.Id, result.Id);
            Assert.Equal(7, existingVariant.StockQuantity);
            _mockProductVariantRepo.Verify(v => v.Add(It.IsAny<ProductVariant>()), Times.Once);
            _mockProductImageRepo.Verify(pi => pi.RemoveRange(It.Is<IEnumerable<ProductImage>>(list => list.Any())), Times.Once);
            _mockProductImageRepo.Verify(pi => pi.AddRange(It.IsAny<IEnumerable<ProductImage>>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldThrow_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _mockProductRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Product?)null);

            await Assert.ThrowsAsync<Exception>(() => _service.DeleteProductAsync(id));
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldRemoveAndComplete_WhenFound()
        {
            var id = Guid.NewGuid();
            var product = new Product { Id = id, ProductName = "TestProduct" };
            _mockProductRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(product);
            _mockProductRepo.Setup(r => r.Remove(It.IsAny<Product>()));
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            await _service.DeleteProductAsync(id);

            _mockProductRepo.Verify(r => r.Remove(product), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }
    }
}
