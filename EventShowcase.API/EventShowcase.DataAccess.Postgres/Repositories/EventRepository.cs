using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventShowcase.DataAccess.Postgres.Repositories
{
    public class EventRepository: IEventRepository
    {
        private readonly EventShowcaseDbContext _dbContext;
        private readonly IValidator<Event> _validator;

        public EventRepository(EventShowcaseDbContext dbConrext, IValidator<Event> validator)
        {
            _dbContext = dbConrext;
            _validator = validator;
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            return await _dbContext.Events.AsNoTracking().ToListAsync();
        }

        public async Task<List<Event>> GetEventsWithUsersAsync()
        {
            return await _dbContext.Events.AsNoTracking().Include(x => x.Users).ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(Guid id)
        {
            var images = await _dbContext.Events.AsNoTracking().Include(u => u.Users).Include(i => i.Images).FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception("Событие не найдено");
            return images;
        }

        public async Task<Event> GetEventsByTitleAsync(string title)
        {
            return await _dbContext
                .Events.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Title == title) ?? throw new Exception("Событие не найдено");
        }

        public async Task<Event> GetEventsByDateAsync(DateTime date)
        {
            return await _dbContext
                .Events.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Date == date) ?? throw new Exception("Событие не найдено");
        }

        public async Task<List<Event>> GetEventsSortedByCategoryAsync()
        {
            return await _dbContext.Events.AsNoTracking().OrderBy(x => x.Category).ToListAsync();
        }

        public async Task<List<Event>> GetEventsSortedByLocationAsync()
        {
            return await _dbContext.Events.AsNoTracking().OrderBy(x => x.Location).ToListAsync();
        }

        public async Task<List<Event>> GetEventsByFilterAsyncAsync(
            DateTime date,
            string location,
            string category
        )
        {
            var query = _dbContext.Events.AsNoTracking();

            query = query.Where(x => x.Date.CompareTo(date) < 0);

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(x => x.Location.Contains(location));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.Category.Contains(category));
            }

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

        public async Task AddEventAsync(
            string title,
            string description,
            DateTime date,
            string location,
            string category,
            int maxUserCount,
            List<Image> images
        )
        {
            var eventEntity = new Event
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                Date = date,
                Location = location,
                Category = category,
                MaxUserCount = maxUserCount,
                Images = images,
            };

            var validate = _validator.Validate(eventEntity);

            if (validate.IsValid)
            {
                await _dbContext.Events.AddAsync(eventEntity);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validate.Errors);
            }
        }

        public async Task AddEventImageAsync(Guid eventId, string link)
        {
            var @event = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == eventId)
                  ?? throw new Exception("Событие не найдено");

            var newimage = new Image
            {
                Id = Guid.NewGuid(),
                Link = link,
                EventId = eventId,
            };

            await _dbContext.AddAsync(newimage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(
            Guid id,
            string title,
            string description,
            DateTime date,
            string location,
            string category,
            int maxUserCount
        )
        {
            await _dbContext
                .Events.Where(x => x.Id == id)
                .ExecuteUpdateAsync(y =>
                    y.SetProperty(e => e.Title, title)
                        .SetProperty(e => e.Description, description)
                        .SetProperty(e => e.Date, date)
                        .SetProperty(e => e.Location, location)
                        .SetProperty(e => e.Category, category)
                        .SetProperty(e => e.MaxUserCount, maxUserCount)
                );
        }

        public async Task DeleteEventAsync(Guid id)
        {
            await _dbContext.Events.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

    }
}
