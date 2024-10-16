using EventShowcase.API.Contracts.Users.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EventShowcase.API.Contracts.Users
{
    public record RegisterUserToEventRequest(Guid IdEvent, string? TokensString) : IRequest<Unit>;
}
