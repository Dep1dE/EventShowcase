using EventShowcase.API.Contracts.Events.Responses;
using EventShowcase.API.Contracts.Image.Responses;
using EventShowcase.API.Contracts.Users.Responses;
using EventShowcase.Application.Contracts.Users.Requests;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Application.Services;
using EventShowcase.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.UseCases.UserUseCases.Handlers.Get
{
    public class GetMyEventsHandler : IRequestHandler<GetMyEventsRequest, List<EventResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UsersService _usersService;

        public GetMyEventsHandler(IUserRepository userRepository, UsersService usersService)
        {
            _userRepository = userRepository;
            _usersService = usersService;
        }

        public async Task<List<EventResponse>> Handle(GetMyEventsRequest request, CancellationToken cancellationToken)
        {
            var user = await _usersService.Auth(request.Token);
            var newUser = await _userRepository.GetUserWhihtEventsAsync(user.Id);
            return newUser.Events.Select(
                e => new EventResponse(
                    e.Id,
                    e.Title,
                    e.Description,
                    e.Date,
                    e.Location,
                    e.Category,
                    e.MaxUserCount,
                    e.Images.Select(
                        i => new ImageResponse(
                            i.Id,
                            i.EventId,
                            i.ImageData,
                            i.ImageType)).ToList(),
                    e.Users.Select(
                        u => new UserResponse(u.Id, u.Name, u.Email, u.IsAdmin)).ToList())).ToList(); 
        }
    }
}
