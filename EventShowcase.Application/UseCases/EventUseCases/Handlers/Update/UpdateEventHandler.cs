using AutoMapper;
using EventShowcase.API.Contracts.Events.Requests;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Models;
using EventShowcase.Core.Validators.Update;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.UseCases.EventUseCases.Handlers.Update
{
    public class UpdateEventHandler : IRequestHandler<UpdateEventRequest, Unit>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public UpdateEventHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEventRequest request, CancellationToken cancellationToken)
        {
            var eventEntity = _mapper.Map<Event>(request);
            await _eventRepository.UpdateAsync(eventEntity);

            return Unit.Value;
        }
    }
}
