using EventShowcase.Application.Interfaces.Auth;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Models;
using EventShowcase.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.Services
{
    public class EventsService
    {
        private readonly IEventRepository _eventRepository;

        public EventsService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await _eventRepository.GetEventsAsync();
        }

        public async Task<Event> GetEventById(Guid idEvent)
        {
            return await _eventRepository.GetEventByIdAsync(idEvent);
        }

        public async Task<Event> GetEventByTitle(string title)
        {
            return await _eventRepository.GetEventsByTitleAsync(title);
        }

        public async Task<Event> GetEventByDate(DateTime date)
        {
            return await _eventRepository.GetEventsByDateAsync(date);
        }

        public async Task AddNewEvent(
            string title,
            string description,
            DateTime date,
            string location,
            string category,
            int maxUserCount,
            List<Image> images)
        {
            await _eventRepository.AddEventAsync(title, description, date, location, category, maxUserCount, images);
        }

        public async Task UpdateEvent(
            Guid id,
            string title,
            string description,
            DateTime date,
            string location,
            string category,
            int maxUserCount)
        {
            await _eventRepository.UpdateEventAsync(id, title, description, date, location, category, maxUserCount);
        }

        public async Task DeleteEvent(Guid idEvent)
        {
            await _eventRepository.DeleteEventAsync(idEvent);
        }

        public async Task<List<Event>> FilterEvents(DateTime date, string location, string category)
        {
            return await _eventRepository.GetEventsByFilterAsyncAsync(date, location, category);
        }

        public async Task AddImage(Guid idEvent, string link)
        {
            await _eventRepository.AddEventImageAsync(idEvent, link);
        }
        public async Task<List<Image>> GetImages(Guid idEvent)
        {
            var @event = await _eventRepository.GetEventByIdAsync(idEvent);
            return @event.Images;
        }
    }
}
