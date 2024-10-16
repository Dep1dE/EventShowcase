using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace EventShowcase.DataAccess.Postgres.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        public ImageRepository(EventShowcaseDbContext dbContext) : base(dbContext) { }
        public async Task<List<Image>> GetImagesByEventIdAsync(Guid idEvent)
        {
            return (await _dbContext.Events.AsNoTracking().Include(u => u.Users).Include(i => i.Images).FirstOrDefaultAsync(x => x.Id == idEvent)).Images;
        } 
    }
}
