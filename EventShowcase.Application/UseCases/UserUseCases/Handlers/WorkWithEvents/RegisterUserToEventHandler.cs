using EventShowcase.API.Contracts.Users;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.UseCases.UserUseCases.Handlers.WorkWithEvents
{
    public class RegisterUserToEventHandler : IRequestHandler<RegisterUserToEventRequest, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUsersService _usersService;

        public RegisterUserToEventHandler(IUserRepository userRepository, IUsersService usersService)
        {
            _userRepository = userRepository;
            _usersService = usersService;   
        }

        public async Task<Unit> Handle(RegisterUserToEventRequest request, CancellationToken cancellationToken)
        {
            var user = await _usersService.Auth(request.Token);
            await _userRepository.RegisterUserToEventAsync(request.IdEvent, user.Id);
            return Unit.Value;
        }
    }
}
