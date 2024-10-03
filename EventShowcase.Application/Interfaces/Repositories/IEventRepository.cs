using EventShowcase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.Interfaces.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetEventsAsync();

        Task<List<Event>> GetEventsWithUsersAsync();

        Task<Event> GetEventByIdAsync(Guid id);

        Task<Event> GetEventsByTitleAsync(string title);
        Task<Event> GetEventsByDateAsync(DateTime date);

        Task<List<Event>> GetEventsSortedByCategoryAsync();

        Task<List<Event>> GetEventsSortedByLocationAsync();

        Task<List<Event>> GetEventsByFilterAsyncAsync(
            DateTime date,
            string location,
            string category
        );

        Task<List<Event>> GetByPageAsync(int page, int pageSize);

        Task<Guid> AddEventAsync(Event eventEntity);

        Task AddEventImageAsync(Guid eventId, Image newImage);

        Task UpdateEventAsync(Event eventEntity);

        Task DeleteEventAsync(Event @event);
    }
}
