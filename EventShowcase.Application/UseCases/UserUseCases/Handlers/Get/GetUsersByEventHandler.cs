using AutoMapper;
using EventShowcase.API.Contracts.Users.Requests;
using EventShowcase.API.Contracts.Users.Responses;
using EventShowcase.Application.Interfaces.Repositories;
using MediatR;


namespace EventShowcase.Application.UseCases.UserUseCases.Handlers.Get
{
    public class GetUsersByEventHandler : IRequestHandler<GetUsersByEventRequest, List<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersByEventHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserResponse>> Handle(GetUsersByEventRequest request, CancellationToken cancellationToken)
        {
            var usersEntities = await _userRepository.GetUsersByEventAsync(request.IdEvent); 
            var userResponse = _mapper.Map<List<UserResponse>>(usersEntities);

            return userResponse;
        }
    }
}
