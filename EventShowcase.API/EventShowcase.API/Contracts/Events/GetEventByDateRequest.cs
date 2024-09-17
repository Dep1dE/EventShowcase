using System;
using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events
{
    public record GetEventByDateRequest(
        [Required] DateTime date);
}
