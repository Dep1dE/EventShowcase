using EventShowcase.API.Contracts.Events.Requests;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Models;
using EventShowcase.Core.Validators.Create;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.UseCases.EventUseCases.Handlers.Create
{
    public class AddNewEventHandler : IRequestHandler<AddNewEventRequest, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly EventCreateValidator _validator;
        private Guid newEventId;

        public AddNewEventHandler(IEventRepository eventRepository, EventCreateValidator validator)
        {
            _eventRepository = eventRepository;
            _validator = validator;
        }

        public async Task<Guid> Handle(AddNewEventRequest request, CancellationToken cancellationToken)
        {
            var eventEntity = new Event
            {
                Id = Guid.NewGuid(),
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
                newEventId = await _eventRepository.AddEventAsync(eventEntity);
            }
            else
            {
                throw new ValidationException(validate.Errors);
            }

            return newEventId;
        }
    }
}
