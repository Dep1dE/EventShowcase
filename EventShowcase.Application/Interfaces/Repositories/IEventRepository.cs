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

        Task AddEventAsync(
            string title,
            string description,
            DateTime date,
            string location,
            string category,
            int maxUserCount,
            List<Image> images
        );

        Task AddEventImageAsync(Guid id, string link);

        Task UpdateEventAsync(
            Guid id,
            string title,
            string description,
            DateTime date,
            string location,
            string category,
            int maxUserCount
        );

        Task DeleteEventAsync(Guid id);
    }
}
