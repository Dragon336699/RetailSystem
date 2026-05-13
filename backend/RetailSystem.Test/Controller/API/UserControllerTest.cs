using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.AspNetCore.Http;
using RetailSystem.API.Controllers;
using RetailSystem.SharedLibrary.Contracts.Users;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.SharedLibrary.Dtos.Users;
using Xunit;
using System.Security.Claims;

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

        [Fact]
        public async Task Login_ShouldReturnOkAndSetCookie_WhenSuccess()
        {
            // Arrange
            var request = new LoginCommand { UserName = "john", Password = "Pass1!" };
            var token = "fake.jwt.token";

            _mockUserService
                .Setup(x => x.Login(It.Is<LoginCommand>(c => c.UserName == request.UserName && c.Password == request.Password)))
                .ReturnsAsync(token);

            // Ensure controller has HttpContext so cookies can be set
            _controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = await _controller.Login(request);

            // Assert
            Assert.IsType<OkResult>(result);
            // Check Set-Cookie header contains jwt
            var headers = _controller.Response.Headers;
            Assert.True(headers.ContainsKey("Set-Cookie"));
            Assert.Contains("jwt=", headers["Set-Cookie"].ToString());

            _mockUserService.Verify(x => x.Login(It.IsAny<LoginCommand>()), Times.Once);
        }

        [Fact]
        public void Logout_ShouldReturnOk_AndDeleteCookie()
        {
            // Arrange
            _controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = _controller.Logout();

            // Assert
            Assert.IsType<OkResult>(result);
            var headers = _controller.Response.Headers;
            Assert.True(headers.ContainsKey("Set-Cookie"));
            Assert.Contains("jwt=", headers["Set-Cookie"].ToString());
        }

        [Fact]
        public async Task GetAdminInfo_ShouldReturnUnauthorized_WhenNoUserIdClaim()
        {
            // Arrange: no user set
            _controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = await _controller.GetAdminInfo();

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task GetAdminInfo_ShouldReturnOkWithUserInfo_WhenUserIdPresent()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var userDto = new UserDto { Id = Guid.Parse(userId), FullName = "Admin", UserName = "admin" };

            _mockUserService.Setup(x => x.GetUserInfo(userId)).ReturnsAsync(userDto);

            var context = new DefaultHttpContext();
            context.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) }));

            _controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = context
            };

            // Act
            var result = await _controller.GetAdminInfo();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<UserDto>(okResult.Value);
            Assert.Equal("Admin", model.FullName);
            _mockUserService.Verify(x => x.GetUserInfo(userId), Times.Once);
        }
    }
}
