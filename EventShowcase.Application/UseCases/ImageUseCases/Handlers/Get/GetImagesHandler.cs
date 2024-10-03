using EventShowcase.API.Contracts.Image.Responses;
using EventShowcase.Application.Contracts.Images.Requests;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.UseCases.ImageUseCases.Handlers.Get
{
    public class GetImagesHandler : IRequestHandler<GetImagesRequest, List<ImageResponse>>
    {
        private readonly IEventRepository _eventRepository;

        public GetImagesHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<ImageResponse>> Handle(GetImagesRequest request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetEventByIdAsync(request.IdEvent);
            return @event.Images.Select(i => new ImageResponse(i.Id, i.EventId, i.ImageData, i.ImageType)).ToList() ;
        }
    }
}
