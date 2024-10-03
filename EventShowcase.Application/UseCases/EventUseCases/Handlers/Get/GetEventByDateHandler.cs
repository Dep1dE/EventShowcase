using EventShowcase.API.Contracts.Events.Requests;
using EventShowcase.API.Contracts.Events.Responses;
using EventShowcase.API.Contracts.Image.Responses;
using EventShowcase.API.Contracts.Users.Responses;
using EventShowcase.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.UseCases.EventUseCases.Handlers.Get
{
    public class GetEventByDateHandler : IRequestHandler<GetEventByDateRequest, EventResponse>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventByDateHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<EventResponse> Handle(GetEventByDateRequest request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetEventsByDateAsync(request.Date);

            return new EventResponse(
                @event.Id,
                @event.Title,
                @event.Description,
                @event.Date,
                @event.Location,
                @event.Category,
                @event.MaxUserCount,
                @event.Images.Select(
                        i => new ImageResponse(
                            i.Id,
                            i.EventId,
                            i.ImageData,
                            i.ImageType)).ToList(),
                    @event.Users.Select(
                        u => new UserResponse(u.Id, u.Name, u.Email, u.IsAdmin)).ToList());
        }
    }
}
