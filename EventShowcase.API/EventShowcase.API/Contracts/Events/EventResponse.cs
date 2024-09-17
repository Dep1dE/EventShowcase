using EventShowcase.API.Contracts.Image;
using EventShowcase.API.Contracts.Users;

namespace EventShowcase.API.Contracts.Events
{
    public record EventResponse(Guid Id, string Title, string Description, DateTime Date, string Location, string Category, int MaxUserCount, List<ImageResponse> Images, List<UserResponse> Users);
}
