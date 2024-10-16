using AutoMapper;
using EventShowcase.API.Contracts.Events.Responses;
using EventShowcase.API.Contracts.Image.Responses;
using EventShowcase.API.Contracts.Users.Responses;
using EventShowcase.Application.Contracts.Users.Requests;
using EventShowcase.Application.Exeptions;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Application.Services;
using EventShowcase.Core.Models;
using MediatR;
using Newtonsoft.Json;


namespace EventShowcase.Application.UseCases.UserUseCases.Handlers.Get
{
    public class GetMyEventsHandler : IRequestHandler<GetMyEventsRequest, List<EventResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UsersService _usersService;

        public GetMyEventsHandler(IUserRepository userRepository, IMapper mapper, UsersService usersService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _usersService = usersService;
        }

        public async Task<List<EventResponse>> Handle(GetMyEventsRequest request, CancellationToken cancellationToken)
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
            
            var userEntityWithEvents = await _userRepository.GetUserWhihtEventsAsync(userEntity.Id)
                ?? throw new EntityNotFoundExeption("Entity not found");
            var eventResponse = _mapper.Map<List<EventResponse>>(userEntityWithEvents.Events);

            return eventResponse;
        }
    }
}
