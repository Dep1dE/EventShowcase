using EventShowcase.API.Contracts.Users;
using EventShowcase.API.Contracts.Users.Responses;
using EventShowcase.Application.Exeptions;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Application.Interfaces.Services;
using EventShowcase.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            User userEntity;
            var tokens = JsonConvert.DeserializeObject<MyTokens>(request.TokensString);

            try
            {
                userEntity = await _usersService.Auth(tokens.Access)
                    ?? throw new EntityNotFoundExeption("Entity not found");
            }
            catch
            {
                try
                {
                    userEntity = await _usersService.Auth(tokens.Refresh)
                        ?? throw new EntityNotFoundExeption("Entity not found");
                }
                catch
                {
                    throw new Exception("Invalid token");
                }
            }

            await _userRepository.RegisterUserToEventAsync(request.IdEvent, userEntity.Id);
            return Unit.Value;
        }
    }
}
