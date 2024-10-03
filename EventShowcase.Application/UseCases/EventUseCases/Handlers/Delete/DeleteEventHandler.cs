using EventShowcase.API.Contracts.Events.Requests;
using EventShowcase.Application.Interfaces.Repositories;
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
        private readonly EventDeleteValidator _validator;


        public DeleteEventHandler(IEventRepository eventRepository, EventDeleteValidator validator)
        {
            _eventRepository = eventRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(DeleteEventRequest request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetEventByIdAsync(request.idEvent);

            var validate = _validator.Validate(@event);

            if (validate.IsValid)
            {
                await _eventRepository.DeleteEventAsync(@event);
            }
            else
            {
                throw new ValidationException(validate.Errors);
            }
            return Unit.Value;
        }
    }
}
