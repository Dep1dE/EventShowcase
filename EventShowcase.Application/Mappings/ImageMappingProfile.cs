using AutoMapper;
using EventShowcase.API.Contracts.Image.Responses;
using EventShowcase.Application.Contracts.Images.Requests;
using EventShowcase.Core.Models;


namespace EventShowcase.Application.Mappings
{
    public class ImageMappingProfile : Profile
    {
        public ImageMappingProfile()
        {
            CreateMap<AddImageRequest, Image>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.IdEvent));

            CreateMap<Image, ImageResponse>();
        }
    }
}
