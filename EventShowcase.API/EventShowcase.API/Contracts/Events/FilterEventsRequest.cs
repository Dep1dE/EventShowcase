using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events
{
    public record FilterEventsRequest(
        [Required] DateTime date,
        [Required] string location,
        [Required] string category);
}
