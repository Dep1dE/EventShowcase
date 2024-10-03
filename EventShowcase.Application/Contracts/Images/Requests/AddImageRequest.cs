using MediatR;

namespace EventShowcase.Application.Contracts.Images.Requests
{
    public record AddImageRequest(
        Guid IdEvent,
        string ImageData,
        string ImageType) : IRequest<Unit>;
}
