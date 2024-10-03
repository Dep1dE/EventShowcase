using EventShowcase.API.Contracts.Image.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EventShowcase.Application.Contracts.Images.Requests
{
    public record GetImagesRequest(Guid IdEvent) : IRequest<List<ImageResponse>>;
}
