using Microsoft.AspNetCore.Mvc;
using RetailSystem.Application.Interfaces.Services;

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

        [HttpGet]
        [Route("admin/customers")]
        public async Task<IActionResult> GetRegisteredCustomer()
        {
            var customers = await _userService.GetRegisteredCustomers();
            return Ok(customers);
        }
    }
}
