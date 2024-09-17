using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events
{
    public record DeleteEventRequest(
        [Required] Guid idEvent);
    
}
