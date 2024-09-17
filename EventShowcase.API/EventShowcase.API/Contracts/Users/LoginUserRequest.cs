using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Users
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password);
}
