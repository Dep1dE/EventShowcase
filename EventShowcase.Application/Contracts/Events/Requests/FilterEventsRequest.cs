using EventShowcase.API.Contracts.Events.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events.Requests
{
    public record FilterEventsRequest(
        DateTime Date,
        string Location,
        string Category) : IRequest<List<EventResponse>>;
}
