using AutoMapper;
using EventShowcase.API.Contracts.Events.Requests;
using EventShowcase.API.Contracts.Events.Responses;
using EventShowcase.API.Contracts.Image.Responses;
using EventShowcase.API.Contracts.Users.Responses;
using EventShowcase.Application.Exeptions;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly IMapper _mapper;

        public GetEventByIdHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<EventResponse> Handle(GetEventByIdRequest request, CancellationToken cancellationToken)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(request.IdEvent)
                ?? throw new EntityNotFoundExeption("Entity not found");
            var eventResponse = _mapper.Map<EventResponse>(eventEntity);

            return eventResponse;
        }
    }
}
