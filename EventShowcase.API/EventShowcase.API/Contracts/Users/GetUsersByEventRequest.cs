using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Users
{
    public record GetUsersByEventRequest(
        [Required] Guid idEvent);
}
