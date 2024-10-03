using EventShowcase.API.Contracts.Events.Responses;
using MediatR;

namespace EventShowcase.API.Contracts.Events.Requests
{
    public record GetAllEventsRequest : IRequest<List<EventResponse>>;
}
