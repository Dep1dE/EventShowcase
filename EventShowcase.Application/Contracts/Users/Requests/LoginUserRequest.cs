using EventShowcase.API.Contracts.Users.Responses;
using MediatR;

namespace EventShowcase.API.Contracts.Users.Requests
{
    public record LoginUserRequest(string Email, string Password) : IRequest<List<string>>;
}
