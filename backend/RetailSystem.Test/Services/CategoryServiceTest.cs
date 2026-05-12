using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using RetailSystem.Application.Interfaces.UnitOfWork;
using RetailSystem.Application.Interfaces.Repositories;
using RetailSystem.Application.Services;
using RetailSystem.Domain.Entities;
using RetailSystem.SharedLibrary.Dtos.Categories;
using AutoMapper;
using Xunit;

namespace RetailSystem.Test.Services
{
    public class CategoryServiceTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ICategoryRepository> _mockCategoryRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryService _service;

        public CategoryServiceTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCategoryRepo = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();

            _mockUnitOfWork.SetupGet(u => u.Categories).Returns(_mockCategoryRepo.Object);

            _service = new CategoryService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ShouldReturnMappedDtos_WhenSuccess()
        {
            var categories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), CategoryName = "Shoes", Description = "Footwear" }
            };

            var expectedDtos = new List<CategoryDto>
            {
                new CategoryDto { Id = categories[0].Id, CategoryName = "Shoes", Description = "Footwear" }
            };

            _mockCategoryRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(categories);
            _mockMapper.Setup(m => m.Map<IEnumerable<CategoryDto>>(categories)).Returns(expectedDtos);

            var result = await _service.GetAllCategoriesAsync();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Shoes", ((List<CategoryDto>)result)[0].CategoryName);
            _mockCategoryRepo.Verify(r => r.GetAllAsync(), Times.Once);
            _mockMapper.Verify(m => m.Map<IEnumerable<CategoryDto>>(categories), Times.Once);
        }

        [Fact]
        public async Task AddCategoryAsync_ShouldAddAndReturnDto()
        {
            var cmd = new CreateCategoryCommand { CategoryName = "NewCat", Description = "Desc" };
            var category = new Category { Id = Guid.NewGuid(), CategoryName = cmd.CategoryName, Description = cmd.Description };
            var expectedDto = new CategoryDto { Id = category.Id, CategoryName = category.CategoryName, Description = category.Description };

            _mockMapper.Setup(m => m.Map<Category>(cmd)).Returns(category);
            _mockCategoryRepo.Setup(r => r.AddAsync(category)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);
            _mockMapper.Setup(m => m.Map<CategoryDto>(category)).Returns(expectedDto);

            var result = await _service.AddCategoryAsync(cmd);

            Assert.Equal(expectedDto.Id, result.Id);
            Assert.Equal("NewCat", result.CategoryName);
            _mockCategoryRepo.Verify(r => r.AddAsync(category), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryAsync_ShouldThrow_WhenNotFound()
        {
            var id = Guid.NewGuid();
            var cmd = new UpdateCategoryCommand { Id = id, CategoryName = "X", Description = "Y" };

            _mockCategoryRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Category?)null);

            await Assert.ThrowsAsync<Exception>(() => _service.UpdateCategoryAsync(cmd));
        }

        [Fact]
        public async Task UpdateCategoryAsync_ShouldUpdateAndReturnDto_WhenFound()
        {
            var id = Guid.NewGuid();
            var existing = new Category { Id = id, CategoryName = "Old", Description = "OldDesc" };
            var cmd = new UpdateCategoryCommand { Id = id, CategoryName = "New", Description = "NewDesc" };
            var expectedDto = new CategoryDto { Id = id, CategoryName = "New", Description = "NewDesc" };

            _mockCategoryRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existing);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);
            _mockMapper.Setup(m => m.Map<CategoryDto>(existing)).Returns(expectedDto);

            var result = await _service.UpdateCategoryAsync(cmd);

            Assert.Equal("New", result.CategoryName);
            Assert.Equal("NewDesc", result.Description);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteCategoryAsync_ShouldThrow_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _mockCategoryRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Category?)null);

            await Assert.ThrowsAsync<Exception>(() => _service.DeleteCategoryAsync(id));
        }

        [Fact]
        public async Task DeleteCategoryAsync_ShouldRemoveAndComplete_WhenFound()
        {
            var id = Guid.NewGuid();
            var category = new Category { Id = id, CategoryName = "ToDelete" };
            _mockCategoryRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(category);
            _mockCategoryRepo.Setup(r => r.Remove(category));
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            await _service.DeleteCategoryAsync(id);

            _mockCategoryRepo.Verify(r => r.Remove(category), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }
    }
}
