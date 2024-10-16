using AutoMapper;
using EventShowcase.API.Contracts.Events.Requests;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Models;
using EventShowcase.Core.Validators.Create;
using FluentValidation;
using MediatR;


namespace EventShowcase.Application.UseCases.EventUseCases.Handlers.Create
{
    public class AddNewEventHandler : IRequestHandler<AddNewEventRequest, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Event> _validator;

        public AddNewEventHandler(IEventRepository eventRepository, IMapper mapper, IValidator<Event> validator)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Guid> Handle(AddNewEventRequest request, CancellationToken cancellationToken)
        {
            var eventEntity = _mapper.Map<Event>(request);
            await _eventRepository.AddAsync(eventEntity);
            return eventEntity.Id;
        }
    }
}
