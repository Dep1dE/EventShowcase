using AutoMapper;
using EventShowcase.API.Contracts.Events.Requests;
using EventShowcase.API.Contracts.Events.Responses;
using EventShowcase.API.Contracts.Image.Responses;
using EventShowcase.API.Contracts.Users.Responses;
using EventShowcase.Application.Interfaces.Repositories;
using MediatR;


namespace EventShowcase.Application.UseCases.EventUseCases.Handlers.Get
{
    public class GetFilterEventsHandler : IRequestHandler<FilterEventsRequest, List<EventResponse>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public GetFilterEventsHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<List<EventResponse>> Handle(FilterEventsRequest request, CancellationToken cancellationToken)
        {
            var eventsEntities = await _eventRepository.GetEventsByFilterAsync(request.Date, request.Location, request.Category);
            var eventResponse = _mapper.Map<List<EventResponse>>(eventsEntities);

            return eventResponse;
        }
    }
}
