using EventShowcase.API.Contracts.Users.Requests;
using EventShowcase.API.Contracts.Users.Responses;
using EventShowcase.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.UseCases.UserUseCases.Handlers.Get
{
    public class GetUsersByEventHandler : IRequestHandler<GetUsersByEventRequest, List<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersByEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponse>> Handle(GetUsersByEventRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsersByEventAsync(request.IdEvent);
            
            return users.Select(u => new UserResponse(u.Id, u.Name, u.Email, u.IsAdmin)).ToList();
        }
    }
}
