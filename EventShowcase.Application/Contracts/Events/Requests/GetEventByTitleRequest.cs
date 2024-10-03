using EventShowcase.API.Contracts.Events.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events.Requests
{
    public record GetEventByTitleRequest(string Title) : IRequest<EventResponse>;
}
