using Microsoft.AspNetCore.Mvc;
using Moq;
using RetailSystem.API.Controllers;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.SharedLibrary.Dtos.Sizes;

namespace RetailSystem.Test.Controller
{
    public class SizeControllerTest
    {
        private readonly Mock<ISizeService> _sizeServiceMock;
        private readonly SizeController _controller;

        public SizeControllerTest()
        {
            _sizeServiceMock = new Mock<ISizeService>();
            _controller = new SizeController(_sizeServiceMock.Object);
        }

        [Fact]
        public async Task GetAllSizes_ShouldReturnOk_WithCorrectData()
        {
            // Arrange
            var fakeSizes = new List<SizeDto>
            {
                new SizeDto { Id = Guid.NewGuid(), SizeNumber = 40 },
                new SizeDto { Id = Guid.NewGuid(), SizeNumber = 41 }
            };

            _sizeServiceMock
                .Setup(x => x.GetAllSizesAsync())
                .ReturnsAsync(fakeSizes);

            // Act
            var result = await _controller.GetAllSizes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<SizeDto>>(okResult.Value);

            Assert.Equal(2, returnValue.Count);

            Assert.Equal(fakeSizes[0].Id, returnValue[0].Id);
            Assert.Equal(40, returnValue[0].SizeNumber);

            Assert.Equal(fakeSizes[1].Id, returnValue[1].Id);
            Assert.Equal(41, returnValue[1].SizeNumber);
        }

        [Fact]
        public async Task GetAllSizes_ShouldReturnOk_WithEmptyList_WhenNoData()
        {
            // Arrange
            _sizeServiceMock
                .Setup(x => x.GetAllSizesAsync())
                .ReturnsAsync(new List<SizeDto>());

            // Act
            var result = await _controller.GetAllSizes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<SizeDto>>(okResult.Value);

            Assert.NotNull(returnValue);
            Assert.Empty(returnValue);
        }
    }
}
