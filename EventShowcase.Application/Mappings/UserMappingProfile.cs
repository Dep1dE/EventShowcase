using AutoMapper;
using EventShowcase.API.Contracts.Users.Requests;
using EventShowcase.API.Contracts.Users.Responses;
using EventShowcase.Core.Models;


namespace EventShowcase.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserResponse>();

            CreateMap<RegisterUserRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }
    }
}
