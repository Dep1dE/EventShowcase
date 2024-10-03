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
        private readonly EventUpdateValidator _validator;
        public UpdateEventHandler(IEventRepository eventRepository, EventUpdateValidator validator)
        {
            _eventRepository = eventRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateEventRequest request, CancellationToken cancellationToken)
        {
            var eventEntity = new Event
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                Location = request.Location,
                Category = request.Category,
                MaxUserCount = request.MaxUserCount,
            };

            var validate = _validator.Validate(eventEntity);

            if (validate.IsValid)
            {
                await _eventRepository.UpdateEventAsync(eventEntity);
            }
            else
            {
                throw new ValidationException(validate.Errors);
            }

            return Unit.Value;
        }
    }
}
