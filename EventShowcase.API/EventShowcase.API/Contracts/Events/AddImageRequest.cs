using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events
{
    public record AddImageRequest(
        [Required] Guid idEvent,
        [Required] string link);
}
