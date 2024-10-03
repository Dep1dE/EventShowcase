namespace EventShowcase.API.Contracts.Users.Responses
{
    public record UserResponse(Guid Id, string Name, string Email, bool IsAdmin);

}
