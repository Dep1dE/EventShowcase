using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Users.Requests
{
    public record DeleteUserInEventRequest(
        Guid IdUser,
        Guid IdEvent) : IRequest<Unit>;
}
