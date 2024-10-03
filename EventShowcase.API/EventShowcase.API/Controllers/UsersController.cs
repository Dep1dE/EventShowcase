using EventShowcase.API.Contracts.Users;
using EventShowcase.API.Contracts.Users.Requests;
using EventShowcase.API.Contracts.Users.Responses;
using EventShowcase.Application.Contracts.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EventShowcase.API.Controllers
{
    [Route("")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("auth")]
        public async Task<IActionResult> Auth()
        {
            if (!HttpContext.Request.Cookies.TryGetValue("tasty-cookies", out var accessToken) || string.IsNullOrEmpty(accessToken))
            {
                if (!HttpContext.Request.Cookies.TryGetValue("refresh-token", out var refreshToken) || string.IsNullOrEmpty(refreshToken))
                {
                    throw new Exception("Токен не найден в cookies.");
                }
                var responseRefresh = await _mediator.Send(new AuthRequest(refreshToken));
                return Ok(responseRefresh);
            }
            var response = await _mediator.Send(new AuthRequest(accessToken));
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            await _mediator.Send(request);
            return Ok(new { Message = "Регистрация успешна" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var response = await _mediator.Send(request);
            HttpContext.Response.Cookies.Delete("tasty-cookies");
            HttpContext.Response.Cookies.Append("tasty-cookies", response[0]);
            HttpContext.Response.Cookies.Delete("refresh-token");
            HttpContext.Response.Cookies.Append("refresh-token", response[1]);
            return Ok(response);
        }

        [Authorize(Policy = "ReadPolicy")]
        [HttpDelete("logout")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("tasty-cookies");
            HttpContext.Response.Cookies.Delete("refresh-token");

            return Ok(new { Message = "Вы вышли из системы" });
        }

        [Authorize(Policy = "ReadPolicy")]
        [HttpPost("register_user_to_event")]
        public async Task<IActionResult> RegisterUserToEvent([FromBody] RegisterUserToEventRequest request)
        {

            if (!HttpContext.Request.Cookies.TryGetValue("tasty-cookies", out var accessToken) || string.IsNullOrEmpty(accessToken))
            {
                if (!HttpContext.Request.Cookies.TryGetValue("refresh-token", out var refreshToken) || string.IsNullOrEmpty(refreshToken))
                {
                    throw new Exception("Токен не найден в cookies.");
                }
                await _mediator.Send(new RegisterUserToEventRequest(request.IdEvent, refreshToken));
                return Ok(new { Message = "Пользователь зарегистрирован на событие" });
            }
            await _mediator.Send(new RegisterUserToEventRequest(request.IdEvent, accessToken));
            return Ok(new { Message = "Пользователь зарегистрирован на событие" });

        }

        [Authorize(Policy = "ReadPolicy")]
        [HttpGet("get_my_events")]
        public async Task<IActionResult> GetMyEvents()
        {
            if (!HttpContext.Request.Cookies.TryGetValue("tasty-cookies", out var accessToken) || string.IsNullOrEmpty(accessToken))
            {
                if (!HttpContext.Request.Cookies.TryGetValue("refresh-token", out var refreshToken) || string.IsNullOrEmpty(refreshToken))
                {
                    throw new Exception("Токен не найден в cookies.");
                }
                var responseRefresh = await _mediator.Send(new GetMyEventsRequest(refreshToken));
                return Ok(responseRefresh);
            }
            var responseAccess = await _mediator.Send(new GetMyEventsRequest(accessToken));
            return Ok(responseAccess);
        }

        [Authorize(Policy = "ReadPolicy")]
        [HttpPost("get_users_by_event")]
        public async Task<IActionResult> GetUsersByEvent([FromBody] GetUsersByEventRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [Authorize(Policy = "ReadPolicy")]
        [HttpPost("delete_user_in_event")]
        public async Task<IActionResult> DeleteUserInEvent([FromBody] DeleteUserInEventRequest request)
        {
            await _mediator.Send(request);
            return Ok(new { Message = "Пользователь удален из события" });
        }
    }
}
