using EventShowcase.API.Contracts.Events.Requests;
using EventShowcase.API.Contracts.Users;
using EventShowcase.Application.Contracts.Images.Requests;
using EventShowcase.Core.Enums;
using EventShowcase.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventShowcase.API.Controllers
{
    [Route("")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get_all_events")]
        public async Task<IActionResult> GetAllEvents()
        {
            var response = await _mediator.Send(new GetAllEventsRequest());
            return Ok(response);
        }

        [Authorize(Policy = "ReadPolicy")]
        [HttpPost("get_event_by_id")]
        public async Task<IActionResult> GetEventById([FromBody] GetEventByIdRequest request)
        {
            var response = await _mediator.Send(request);
            if (response == null)
                return NotFound("Событие не найдено");

            return Ok(response);
        }

        [Authorize(Policy = "ReadPolicy")]
        [HttpPost("get_event_by_title")]
        public async Task<IActionResult> GetEventByTitle([FromBody] GetEventByTitleRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [Authorize(Policy = "ReadPolicy")]
        [HttpPost("get_event_by_date")]
        public async Task<IActionResult> GetEventByDate([FromBody] GetEventByDateRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [Authorize(Policy = "CreatePolicy")]
        [HttpPost("add_new_event")]
        public async Task<IActionResult> AddNewEvent([FromBody] AddNewEventRequest request)
        {
            await _mediator.Send(request);
            return Ok(new { Message = "Событие добавлено" });
        }

        [Authorize(Policy = "UpdatePolicy")]
        [HttpPost("update_event")]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventRequest request)
        {
            await _mediator.Send(request);
            return Ok(new { Message = "Событие изменено" });
        }

        [Authorize(Policy = "DeletePolicy")]
        [HttpPost("delete_event")]
        public async Task<IActionResult> DeleteEvent([FromBody] GetEventByIdRequest request)
        {
            await _mediator.Send(request);
            return Ok(new { Message = "Событие удалено" });
        }

        [HttpPost("filter_events")]
        public async Task<IActionResult> FilterEvents([FromBody] FilterEventsRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [Authorize(Policy = "UpdatePolicy")]
        [HttpPost("add_image")]
        public async Task<IActionResult> AddImage([FromBody] AddImageRequest request)
        {
            await _mediator.Send(request);
            return Ok(new { Message = "Картинка добавлена" });
        }

        [Authorize(Policy = "ReadPolicy")]
        [HttpPost("get_event_images")]
        public async Task<IActionResult> GetImages([FromBody] GetImagesRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
