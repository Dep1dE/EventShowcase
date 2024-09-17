using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events
{
    public record GetImagesRequest(
        [Required] Guid idEvent);
}
