using EventShowcase.Core.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events.Requests
{
    public record UpdateEventRequest(
        Guid Id,
        string Title,
        string Description,
        DateTime Date,
        string Location,
        string Category,
        int MaxUserCount) : IRequest<Unit>;
}
