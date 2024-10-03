using EventShowcase.Core.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events.Requests
{
    public record AddNewEventRequest(
        string Title,
        string Description,
        DateTime Date,
        string Location,
        string Category,
        int MaxUserCount) : IRequest<Guid>;
}
