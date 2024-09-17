using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events
{
    public record GetEventByIdRequest(
        [Required] Guid idEvent);
}
