using AutoMapper;
using EventShowcase.Application.Contracts.Images.Requests;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Models;
using EventShowcase.Core.Validators.Create;
using FluentValidation;
using MediatR;


namespace EventShowcase.Application.UseCases.ImageUseCases.Handlers.Create
{
    public class AddImageHandler : IRequestHandler<AddImageRequest, Unit>
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public AddImageHandler(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddImageRequest request, CancellationToken cancellationToken)
        {
            var imageEntity = _mapper.Map<Image>(request);
            await _imageRepository.AddAsync(imageEntity);

            return Unit.Value;
        }
    }
}
