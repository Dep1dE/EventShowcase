using EventShowcase.Core.Models;

namespace EventShowcase.Application.Interfaces.Repositories
{
    public interface IImageRepository : IBaseRepository<Image>
    {
        Task<List<Image>> GetImagesByEventIdAsync(Guid idEvent);
    }
}
