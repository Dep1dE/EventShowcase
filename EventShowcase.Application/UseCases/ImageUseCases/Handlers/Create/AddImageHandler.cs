using EventShowcase.Application.Contracts.Images.Requests;
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

namespace EventShowcase.Application.UseCases.ImageUseCases.Handlers.Create
{
    public class AddImageHandler : IRequestHandler<AddImageRequest, Unit>
    {
        private readonly IEventRepository _eventRepository;
        private readonly ImageCreateValidator _validator;

        public AddImageHandler(IEventRepository eventRepository, ImageCreateValidator validator)
        {
            _eventRepository = eventRepository;
            _validator = validator;
        }

        public async Task<Unit> Handle(AddImageRequest request, CancellationToken cancellationToken)
        {
            var m =request.ImageData;
            var newImage = new Image
            {
                Id = Guid.NewGuid(),
                EventId = request.IdEvent,
                ImageData = request.ImageData,
                ImageType = request.ImageType,
            };

            var validate = _validator.Validate(newImage);

            if (validate.IsValid)
            {
                await _eventRepository.AddEventImageAsync(request.IdEvent, newImage);
            }
            else
            {
                throw new ValidationException(validate.Errors);
            }
            return Unit.Value;
        }
    }
}
