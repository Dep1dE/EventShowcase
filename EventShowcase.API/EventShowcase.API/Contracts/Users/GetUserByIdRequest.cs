using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Users
{
    public record GetUserByIdRequest(
        [Required] Guid idUser);
}
