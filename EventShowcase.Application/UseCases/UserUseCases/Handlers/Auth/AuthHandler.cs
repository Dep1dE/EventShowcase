using EventShowcase.API.Contracts.Users.Responses;
using EventShowcase.Application.Contracts.Users.Requests;
using EventShowcase.Application.Services;
using EventShowcase.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.UseCases.UserUseCases.Handlers.Auth
{
    public class AuthHandler : IRequestHandler<AuthRequest, UserResponse>
    {
        private readonly UsersService _usersService;

        public AuthHandler(UsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task<UserResponse> Handle(AuthRequest request, CancellationToken cancellationToken)
        {
            var user = await _usersService.Auth(request.Token);

            var userResponse = new UserResponse(user.Id, user.Name, user.Email, user.IsAdmin);

            return userResponse;
        }
    }
}
