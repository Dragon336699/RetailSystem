using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using RetailSystem.Application.Services;
using RetailSystem.Domain.Entities;
using RetailSystem.SharedLibrary.Dtos.Users;
using Xunit;

namespace RetailSystem.Test.Services
{
    public class UserServiceTest
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserService _service;

        public UserServiceTest()
        {
            var store = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            _mockMapper = new Mock<IMapper>();

            _service = new UserService(_mockUserManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task RegisterCustomerAsync_ShouldCreateUserAndAddRole_WhenSuccess()
        {
            var cmd = new RegisterCustomerCommand { FullName = "John", UserName = "john", Password = "Pass1!" };

            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<User>(), cmd.Password)).ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), "Customer")).ReturnsAsync(IdentityResult.Success);

            await _service.RegisterCustomerAsync(cmd);

            _mockUserManager.Verify(um => um.CreateAsync(It.Is<User>(u => u.UserName == cmd.UserName && u.FullName == cmd.FullName), cmd.Password), Times.Once);
            _mockUserManager.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), "Customer"), Times.Once);
        }

        [Fact]
        public async Task RegisterCustomerAsync_ShouldThrow_WhenCreateFails()
        {
            var cmd = new RegisterCustomerCommand { FullName = "John", UserName = "john", Password = "Pass1!" };

            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<User>(), cmd.Password)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            await Assert.ThrowsAsync<Exception>(() => _service.RegisterCustomerAsync(cmd));
        }

        [Fact]
        public async Task GetRegisteredCustomers_ShouldReturnMappedDtos()
        {
            var users = new List<User> { new User { Id = Guid.NewGuid(), UserName = "a", FullName = "A" } };
            var dtos = new List<UserDto> { new UserDto { Id = users[0].Id, UserName = "a", FullName = "A" } };

            _mockUserManager.Setup(um => um.GetUsersInRoleAsync("Customer")).ReturnsAsync(users);
            _mockMapper.Setup(m => m.Map<List<UserDto>>(users)).Returns(dtos);

            var result = await _service.GetRegisteredCustomers();

            Assert.Single(result);
            Assert.Equal("A", result[0].FullName);
            _mockUserManager.Verify(um => um.GetUsersInRoleAsync("Customer"), Times.Once);
        }
    }
}
