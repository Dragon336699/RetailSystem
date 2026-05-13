using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailSystem.API.Contracts.Users;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.SharedLibrary.Dtos.Users;
using System.Security.Claims;

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRegisteredCustomer()
        {
            var customers = await _userService.GetRegisteredCustomers();
            return Ok(customers);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            var jwt = await _userService.Login(request);

            HandleJwt(jwt);

            return Ok();
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return Ok();
        }

        [HttpGet]
        [Route("admin/info")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return Unauthorized();

            var usersInfo = await _userService.GetUserInfo(userId);

            return Ok(usersInfo);
        }

        private void HandleJwt(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("jwt", token, cookieOptions);
        }
    }
}
