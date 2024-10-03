using EventShowcase.API.Contracts.Events.Requests;
using EventShowcase.API.Contracts.Events.Responses;
using EventShowcase.API.Contracts.Image.Responses;
using EventShowcase.API.Contracts.Users.Responses;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.UseCases.EventUseCases.Handlers.Get
{
    public class GetEventByIdHandler : IRequestHandler<GetEventByIdRequest, EventResponse>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventByIdHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<EventResponse> Handle(GetEventByIdRequest request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetEventByIdAsync(request.IdEvent);
                
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
