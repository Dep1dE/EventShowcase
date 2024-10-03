using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EventShowcase.API.Contracts.Users.Requests
{
    public record RegisterUserRequest(
        string UserName,
        string Email,
        string Password) : IRequest<Unit>;

}
