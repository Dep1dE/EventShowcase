using EventShowcase.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events
{
    public record UpdateEventRequest(
        [Required] Guid Id,
        [Required] string Title,
        [Required] string Description,
        [Required] DateTime Date,
        [Required] string Location,
        [Required] string Category,
        [Required] int MaxUserCount);
}
