using AutoMapper;
using EventShowcase.API.Contracts.Events.Requests;
using EventShowcase.API.Contracts.Events.Responses;
using EventShowcase.Application.Exeptions;
using EventShowcase.Application.Interfaces.Repositories;
using MediatR;

namespace EventShowcase.Application.UseCases.EventUseCases.Handlers.Get
{
    public class GetEventByDateHandler : IRequestHandler<GetEventByDateRequest, EventResponse>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public GetEventByDateHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;

        }

        public async Task<EventResponse> Handle(GetEventByDateRequest request, CancellationToken cancellationToken)
        {
            var eventEntity = await _eventRepository.GetEventsByDateAsync(request.Date)
                ?? throw new EntityNotFoundExeption("Entity not found");
            var eventResponse = _mapper.Map<EventResponse>(eventEntity);

            return eventResponse;
        }
    }
}
