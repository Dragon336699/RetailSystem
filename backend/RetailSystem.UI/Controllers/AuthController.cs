using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RetailSystem.SharedLibrary.Contracts.Users;
using RetailSystem.SharedLibrary.Dtos.Users;
using RetailSystem.UI.Services.Intefaces;

namespace RetailSystem.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        public AuthController(IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterCustomerRequest request)
        {
            try
            {
                await _userApiClient.RegisterUser(request);
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Register", request);
            }
        }

    }
}
