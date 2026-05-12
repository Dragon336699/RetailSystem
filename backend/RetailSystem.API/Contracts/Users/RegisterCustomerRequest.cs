using System.ComponentModel.DataAnnotations;

namespace RetailSystem.API.Contracts.Users
{
    public class RegisterCustomerRequest
    {
        public required string FullName { get; init; }
        public required string UserName { get; init; }
        public required string Password { get; init; }
        [Compare("Password", ErrorMessage = "Confirm password is not match with password")]
        public required string ConfirmPassword { get; init; }
    }
}
