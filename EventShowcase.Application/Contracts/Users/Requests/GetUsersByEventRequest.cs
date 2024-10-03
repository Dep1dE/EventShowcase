using EventShowcase.API.Contracts.Users.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Users.Requests
{
    public record GetUsersByEventRequest(Guid IdEvent) : IRequest<List<UserResponse>>;
}
