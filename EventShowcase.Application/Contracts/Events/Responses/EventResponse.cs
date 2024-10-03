using EventShowcase.API.Contracts.Image.Responses;
using EventShowcase.API.Contracts.Users.Responses;

namespace EventShowcase.API.Contracts.Events.Responses
{
    public record EventResponse(
        Guid Id, 
        string Title, 
        string Description, 
        DateTime Date, 
        string Location, 
        string Category, 
        int MaxUserCount, 
        List<ImageResponse> Images, 
        List<UserResponse> Users
        );
}
