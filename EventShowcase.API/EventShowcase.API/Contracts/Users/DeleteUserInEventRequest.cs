using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Users
{
    public record DeleteUserInEventRequest(
        [Required] Guid idUser,
        [Required] Guid idEvent);
}
