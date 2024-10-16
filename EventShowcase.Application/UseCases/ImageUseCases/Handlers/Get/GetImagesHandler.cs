using AutoMapper;
using EventShowcase.API.Contracts.Image.Responses;
using EventShowcase.Application.Contracts.Images.Requests;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Application.Services;
using MediatR;


namespace EventShowcase.Application.UseCases.ImageUseCases.Handlers.Get
{
    public class GetImagesHandler : IRequestHandler<GetImagesRequest, List<ImageResponse>>
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public GetImagesHandler(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<List<ImageResponse>> Handle(GetImagesRequest request, CancellationToken cancellationToken)
        {
            var imagesEntities = await _imageRepository.GetImagesByEventIdAsync(request.IdEvent); 
            var imagesResponse = _mapper.Map<List<ImageResponse>>(imagesEntities);

            return imagesResponse;
        }
    }
}
