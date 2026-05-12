using Microsoft.AspNetCore.Mvc;
using RetailSystem.API.Contracts.Users;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.SharedLibrary.Dtos.Users;

namespace RetailSystem.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerRequest request)
        {
            await _userService.RegisterCustomerAsync(new RegisterCustomerCommand
            {
                FullName = request.FullName,
                UserName = request.UserName,
                Password = request.Password
            });

            return Ok();
        }

        [HttpGet]
        [Route("admin/customers")]
        public async Task<IActionResult> GetRegisteredCustomer()
        {
            var customers = await _userService.GetRegisteredCustomers();
            return Ok(customers);
        }
    }
}
