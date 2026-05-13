using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RetailSystem.SharedLibrary.Contracts.Users;
using RetailSystem.UI.Controllers;
using RetailSystem.UI.Services.Intefaces;
using Xunit;

namespace RetailSystem.Test.Controller.API
{
    public class AuthControllerTest
    {
        private readonly AuthController _controller;
        private readonly Mock<IUserApiClient> _mockUserApi;

        public AuthControllerTest()
        {
            _mockUserApi = new Mock<IUserApiClient>();
            _controller = new AuthController(_mockUserApi.Object);
        }

        [Fact]
        public void Register_ShouldReturnView()
        {
            var result = _controller.Register();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Login_ShouldReturnView()
        {
            var result = _controller.Login();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task RegisterUser_ShouldRedirectToLogin_OnSuccess()
        {
            var request = new RegisterCustomerRequest
            {
                FullName = "Test",
                UserName = "test",
                Password = "Pass1!",
                ConfirmPassword = "Pass1!"
            };

            _mockUserApi.Setup(x => x.RegisterUser(request)).Returns(Task.CompletedTask);

            var result = await _controller.RegisterUser(request);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Auth", redirect.ControllerName);
            _mockUserApi.Verify(x => x.RegisterUser(request), Times.Once);
        }

        [Fact]
        public async Task RegisterUser_ShouldReturnViewWithModelAndModelError_OnException()
        {
            var request = new RegisterCustomerRequest
            {
                FullName = "Test",
                UserName = "test",
                Password = "Pass1!",
                ConfirmPassword = "Pass1!"
            };

            var ex = new Exception("Registration failed");
            _mockUserApi.Setup(x => x.RegisterUser(request)).ThrowsAsync(ex);

            var result = await _controller.RegisterUser(request);

            var view = Assert.IsType<ViewResult>(result);
            Assert.Equal("Register", view.ViewName);
            Assert.Equal(request, view.Model);
            Assert.False(_controller.ModelState.IsValid);
            Assert.True(_controller.ModelState.ContainsKey(string.Empty));
            var errors = _controller.ModelState[string.Empty].Errors;
            Assert.Contains(errors, e => e.ErrorMessage == ex.Message);
        }
    }
}
