using EventShowcase.API.Contracts.Users.Requests;
using EventShowcase.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.UseCases.UserUseCases.Handlers.Delete
{
    public class DeleteUserInEventHandler : IRequestHandler<DeleteUserInEventRequest, Unit>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserInEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteUserInEventRequest request, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteUserInEventAsync(request.IdUser, request.IdEvent);
            return Unit.Value;
        }
    }
}
