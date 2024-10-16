using AutoMapper;
using EventShowcase.API.Contracts.Users.Responses;
using EventShowcase.Application.Contracts.Users.Requests;
using EventShowcase.Application.Exeptions;
using EventShowcase.Application.Services;
using EventShowcase.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace EventShowcase.Application.UseCases.UserUseCases.Handlers.Auth
{
    public class AuthHandler : IRequestHandler<AuthRequest, UserResponse>
    {
        private readonly UsersService _usersService;
        private readonly IMapper _mapper;

        public AuthHandler(UsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(AuthRequest request, CancellationToken cancellationToken)
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

            var userResponse = _mapper.Map<UserResponse>(userEntity);

            return userResponse;
        }
    }
}
