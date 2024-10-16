using EventShowcase.API.Contracts.Users;
using EventShowcase.API.Contracts.Users.Requests;
using EventShowcase.Application.Contracts.Users.Requests;
using EventShowcase.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
            HttpContext.Request.Cookies.TryGetValue("tasty-cookies", out var tokensString);
            var response = await _mediator.Send(new AuthRequest(tokensString));
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

            MyTokens tokens = new MyTokens();
            tokens.Access = response.Item1;
            tokens.Refresh=response.Item2;

            HttpContext.Response.Cookies.Delete("tasty-cookies");
            HttpContext.Response.Cookies.Append("tasty-cookies", Newtonsoft.Json.JsonConvert.SerializeObject(tokens));
            return Ok(response);
        }

        [Authorize(Policy = "ReadPolicy")]
        [HttpDelete("logout")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("tasty-cookies");
            return Ok(new { Message = "Вы вышли из системы" });
        }

        [Authorize(Policy = "ReadPolicy")]
        [HttpPost("register_user_to_event")]
        public async Task<IActionResult> RegisterUserToEvent([FromBody] RegisterUserToEventRequest request)
        {
            HttpContext.Request.Cookies.TryGetValue("tasty-cookies", out var tokensString); 
            await _mediator.Send(new RegisterUserToEventRequest(request.IdEvent, tokensString));
            return Ok(new { Message = "Пользователь зарегистрирован на событие" });
        }

        [Authorize(Policy = "ReadPolicy")]
        [HttpGet("get_my_events")]
        public async Task<IActionResult> GetMyEvents()
        {
            HttpContext.Request.Cookies.TryGetValue("tasty-cookies", out var tokensString);
            var responseAccess = await _mediator.Send(new GetMyEventsRequest(tokensString));
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
