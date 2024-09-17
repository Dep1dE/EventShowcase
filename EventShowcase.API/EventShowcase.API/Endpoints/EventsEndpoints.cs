using EventShowcase.API.Contracts.Events;
using EventShowcase.API.Contracts.Image;
using EventShowcase.API.Contracts.Users;
using EventShowcase.API.Extensions;
using EventShowcase.Application.Services;
using EventShowcase.Core.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventShowcase.API.Endpoints
{
    public static class EventsEndpoints
    {
        public static IEndpointRouteBuilder MapEventsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/get_all_events", GetAllEvents);
            app.MapPost("/get_event_by_id", GetEventById).RequireAuthorization().RequirePermissions(UserPermissions.Read); 
            app.MapPost("/get_event_by_title", GetEventByTitle).RequireAuthorization().RequirePermissions(UserPermissions.Read);
            app.MapPost("/get_event_by_date", GetEventByDate).RequireAuthorization().RequirePermissions(UserPermissions.Read);
            app.MapPost("/add_new_event", AddNewEvent);
            app.MapPost("/update_event", UpdateEvent).RequireAuthorization().RequirePermissions(UserPermissions.Update); 
            app.MapPost("/delete_event", DeleteEvent).RequireAuthorization().RequirePermissions(UserPermissions.Delete); 
            app.MapPost("/filter_events", FilterEvents);
            app.MapPost("/add_image", AddImage).RequireAuthorization().RequirePermissions(UserPermissions.Update); 
            app.MapPost("/get_event_images", GetImages).RequireAuthorization().RequirePermissions(UserPermissions.Read);

            return app;
        }

        public static async Task<IResult> GetAllEvents(EventsService eventsService)
        {
            try
            {
                var events = await eventsService.GetAllEvents();

                return Results.Ok(events);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> GetEventById(GetEventByIdRequest request,EventsService eventsService)
        {
            try
            {
                var @event = await eventsService.GetEventById(request.idEvent);

                if (@event == null)
                {
                    return Results.NotFound("Событие не найдено");
                }

                var imageResponses = @event.Images
                    .Select(image => new ImageResponse(image.Id, image.EventId,image.Link))
                    .ToList();
                var userResponse = @event.Users
                    .Select(user => new UserResponse(user.Id))
                    .ToList();

                var eventResponse = new EventResponse(
                    @event.Id,
                    @event.Title,
                    @event.Description,
                    @event.Date,
                    @event.Location,
                    @event.Category,
                    @event.MaxUserCount,
                    imageResponses,
                    userResponse
                );

                return Results.Ok(eventResponse);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> GetEventByTitle(GetEventByTitleRequest request, EventsService eventsService)
        {
            try
            {
                var @event = await eventsService.GetEventByTitle(request.title);

                return Results.Ok(@event);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> GetEventByDate(GetEventByDateRequest request, EventsService eventsService)
        {
            try
            {
                var @event = await eventsService.GetEventByDate(request.date);

                return Results.Ok(@event);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> AddNewEvent(AddNewEventRequest request, EventsService eventsService)
        {
            try
            {
                await eventsService.AddNewEvent(request.Title, request.Description,
                    request.Date, request.Location, request.Category, request.MaxUserCount, request.images);
                return Results.Ok(new { Massage = "Событие добавлено" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> UpdateEvent(UpdateEventRequest request, EventsService eventsService)
        {
            try
            {
                await eventsService.UpdateEvent(request.Id, request.Title, request.Description,
                    request.Date, request.Location, request.Category, request.MaxUserCount);
                return Results.Ok(new { Massage = "Событие изменено" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> DeleteEvent(GetEventByIdRequest request, EventsService eventsService)
        {
            try
            {
                await eventsService.DeleteEvent(request.idEvent);

                return Results.Ok(new { Massage = "Событие удалено" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> FilterEvents(FilterEventsRequest request, EventsService eventsService)
        {
            try
            {
                var events = await eventsService.FilterEvents(request.date, request.location, request.category);
                    
                return Results.Ok(events);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> AddImage(AddImageRequest request, EventsService eventsService)
        {
            try
            {
                await eventsService.AddImage(request.idEvent, request.link);
                return Results.Ok(new { Massage = "Картинка добавлена" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> GetImages(GetImagesRequest request, EventsService eventsService)
        {
            try
            {
                var images = (await eventsService.GetImages(request.idEvent)).Select(i => new ImageResponse(i.Id, i.EventId, i.Link));
                return Results.Ok(images);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

    }
}
