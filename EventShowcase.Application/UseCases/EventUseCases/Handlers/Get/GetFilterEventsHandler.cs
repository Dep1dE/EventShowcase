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
    public class GetFilterEventsHandler : IRequestHandler<FilterEventsRequest, List<EventResponse>>
    {
        private readonly IEventRepository _eventRepository;

        public GetFilterEventsHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<EventResponse>> Handle(FilterEventsRequest request, CancellationToken cancellationToken)
        {
            var events = await _eventRepository.GetEventsByFilterAsyncAsync(request.Date, request.Location, request.Category);
            return events.Select(
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
