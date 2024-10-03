namespace EventShowcase.API.Contracts.Image.Responses
{
    public record ImageResponse(Guid Id, Guid EventId, string ImageData, string ImageType);
}
