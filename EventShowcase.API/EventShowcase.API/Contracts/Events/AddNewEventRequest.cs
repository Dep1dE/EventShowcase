using EventShowcase.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events
{
    public record AddNewEventRequest(
        [Required] string Title,
        [Required] string Description,
        [Required] DateTime Date,
        [Required] string Location,
        [Required] string Category,
        [Required] int MaxUserCount,
        [Required] List<Core.Models.Image> images);
}
