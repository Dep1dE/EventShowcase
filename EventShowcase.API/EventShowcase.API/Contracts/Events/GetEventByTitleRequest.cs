using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events
{
    public record GetEventByTitleRequest(
        [Required] string title);
}
