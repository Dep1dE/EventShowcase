using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EventShowcase.DataAccess.Postgres.Repositories
{
    public class EventRepository: BaseRepository<Event>, IEventRepository
    {

        public EventRepository(EventShowcaseDbContext dbContext) : base(dbContext) { }

        public async Task<List<Event>> GetEventsWithUsersAsync()
        {
            return await _dbContext.Events.AsNoTracking().Include(x => x.Users).ToListAsync();
        }
        public override async Task<Event?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Events.AsNoTracking().Include(u => u.Users).Include(i => i.Images).FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<Event?> GetEventsByTitleAsync(string title)
        {
            return await _dbContext
                .Events.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task<Event?> GetEventsByDateAsync(DateTime date)
        {
            return await _dbContext
                .Events.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Date == date);
        }

        public async Task<List<Event>> GetEventsSortedByCategoryAsync()
        {
            return await _dbContext.Events.AsNoTracking().OrderBy(x => x.Category).ToListAsync();
        }

        public async Task<List<Event>> GetEventsSortedByLocationAsync()
        {
            return await _dbContext.Events.AsNoTracking().OrderBy(x => x.Location).ToListAsync();
        }

        public async Task<List<Event>> GetEventsByFilterAsync(
                DateTime date,
                string location,
                string category
            )
        {
            var query = _dbContext.Events.AsNoTracking()
                .Where(x => x.Date.CompareTo(date) < 0);

            query = query.Where(x => string.IsNullOrEmpty(location) || x.Location.Contains(location));
            query = query.Where(x => string.IsNullOrEmpty(category) || x.Category.Contains(category));

            return await query.ToListAsync();
        }

        public async Task<List<Event>> GetByPageAsync(int page, int pageSize)
        {
            return await _dbContext
                .Events.AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
