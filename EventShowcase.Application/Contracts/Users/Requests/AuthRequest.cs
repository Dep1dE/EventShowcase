using EventShowcase.API.Contracts.Users.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EventShowcase.Application.Contracts.Users.Requests
{
    public record AuthRequest(string Token) : IRequest<UserResponse>;
}
