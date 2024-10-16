using EventShowcase.API.Contracts.Events.Requests;
using EventShowcase.Application.Exeptions;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Models;
using EventShowcase.Core.Validators.Delete;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.UseCases.EventUseCases.Handlers.Delete
{
    public class DeleteEventHandler : IRequestHandler<DeleteEventRequest, Unit>
    {
        private readonly IEventRepository _eventRepository;


        public DeleteEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Unit> Handle(DeleteEventRequest request, CancellationToken cancellationToken)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(request.idEvent)
                ?? throw new EntityNotFoundExeption("Entity not found");

            await _eventRepository.DeleteAsync(eventEntity);

            return Unit.Value;
        }
    }
}
