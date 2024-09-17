using EventShowcase.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Users
{
    public record RegisterUserToEventRequest(
        [Required] Guid idEvent);
}
