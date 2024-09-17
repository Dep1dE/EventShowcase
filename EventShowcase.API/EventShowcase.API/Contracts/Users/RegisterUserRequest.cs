using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Users
{
    public record RegisterUserRequest(
        [Required] string UserName,
        [Required] string Email,
        [Required] string Password);

}
