using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using RetailSystem.Application.Interfaces.UnitOfWork;
using RetailSystem.Application.Interfaces.Repositories;
using RetailSystem.Application.Services;
using RetailSystem.Domain.Entities;
using Xunit;

namespace RetailSystem.Test.Services
{
    public class SizeServiceTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ISizeRepository> _mockSizeRepo;
        private readonly SizeService _service;

        public SizeServiceTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockSizeRepo = new Mock<ISizeRepository>();
            _mockUnitOfWork.SetupGet(u => u.Sizes).Returns(_mockSizeRepo.Object);
            _service = new SizeService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllSizesAsync_ShouldReturnOrderedSizes()
        {
            var sizes = new List<Size>
            {
                new Size { Id = Guid.NewGuid(), SizeNumber = 42.0m },
                new Size { Id = Guid.NewGuid(), SizeNumber = 38.5m },
                new Size { Id = Guid.NewGuid(), SizeNumber = 40.0m }
            };

            _mockSizeRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(sizes);

            var result = await _service.GetAllSizesAsync();

            Assert.Equal(3, result.Count);
            Assert.Equal(38.5m, result[0].SizeNumber);
            Assert.Equal(40.0m, result[1].SizeNumber);
            Assert.Equal(42.0m, result[2].SizeNumber);
            _mockSizeRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllSizesAsync_ShouldReturnEmptyList_WhenNoSizes()
        {
            _mockSizeRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Size>());

            var result = await _service.GetAllSizesAsync();

            Assert.Empty(result);
        }
    }
}
