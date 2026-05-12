using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RetailSystem.API.Controllers;
using RetailSystem.API.Contracts.Users;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.SharedLibrary.Dtos.Users;
using Xunit;

namespace RetailSystem.Test.Controller.API
{
    public class UserControllerTest
    {
        private readonly UserController _controller;
        private readonly Mock<IUserService> _mockUserService;

        public UserControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UserController(_mockUserService.Object);
        }

        [Fact]
        public async Task RegisterCustomer_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var request = new RegisterCustomerRequest
            {
                FullName = "Long",
                UserName = "long3366",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };

            _mockUserService
                .Setup(x => x.RegisterCustomerAsync(It.Is<RegisterCustomerCommand>(c =>
                    c.FullName == request.FullName &&
                    c.UserName == request.UserName &&
                    c.Password == request.Password)))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.RegisterCustomer(request);

            // Assert
            Assert.IsType<OkResult>(result);

            _mockUserService.Verify(x => x.RegisterCustomerAsync(It.IsAny<RegisterCustomerCommand>()), Times.Once);
        }

        [Fact]
        public async Task GetRegisteredCustomer_ShouldReturnCustomers_WhenSuccess()
        {
            // Arrange
            var users = new List<UserDto>
            {
                new UserDto
                {
                    Id = Guid.NewGuid(),
                    FullName = "Long",
                    UserName = "long3366"
                }
            };

            _mockUserService
                .Setup(x => x.GetRegisteredCustomers())
                .ReturnsAsync(users);

            // Act
            var result = await _controller.GetRegisteredCustomer();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);

            Assert.Single(model);
            Assert.Equal("Long", model.First().FullName);

            _mockUserService.Verify(x => x.GetRegisteredCustomers(), Times.Once);
        }
    }
}
