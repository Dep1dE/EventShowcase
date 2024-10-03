using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Events.Requests
{
    public record DeleteEventRequest(Guid idEvent) : IRequest<Unit>;

}
