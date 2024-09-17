using EventShowcase.API.Contracts.Events;
using EventShowcase.API.Contracts.Image;
using EventShowcase.API.Contracts.Users;
using EventShowcase.API.Extensions;
using EventShowcase.Application.Services;
using EventShowcase.Core.Enums;
using EventShowcase.Core.Models;

namespace EventShowcase.API.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app) 
        {
            app.MapGet("/auth", Auth);
            app.MapPost("/register", Register);
            app.MapPost("/login", Login);
            app.MapPost("/register_user_to_event", RegisterUserToEvent).RequirePermissions(UserPermissions.Read);
            app.MapGet("/get_my_events", GetMyEvents).RequirePermissions(UserPermissions.Read);
            app.MapPost("/get_users_by_event", GetUsersByEvent).RequirePermissions(UserPermissions.Read);
            app.MapPost("/get_user_by_id", GetUserById).RequirePermissions(UserPermissions.Update);
            app.MapPost("/delete_user_in_event", DeleteUserInEvent).RequirePermissions(UserPermissions.Read);
            app.MapDelete("/logout", Logout).RequirePermissions(UserPermissions.Read); 

            return app;
        }

        public static async Task<IResult> Auth(UsersService usersService, HttpContext context)
        {
            try
            {
                var user = await usersService.Auth(context);
                return Results.Ok(user);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> Register(RegisterUserRequest request,UsersService usersService)
        {
            try
            {
                await usersService.Register(request.UserName, request.Email, request.Password);
                return Results.Ok(new { Massage = "Регистрация успешна" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> Login(LoginUserRequest request, UsersService usersService, HttpContext context)
        {
            var token = await usersService.Login(request.Email, request.Password);

            context.Response.Cookies.Append("tasty-cookies", token);

            return Results.Ok();
        }

        public static IResult Logout(HttpContext context)
        {
            context.Response.Cookies.Delete("tasty-cookies");

            return Results.Ok();
        }

        public static async Task<IResult> RegisterUserToEvent(RegisterUserToEventRequest request, UsersService usersService, HttpContext context)
        {
            try
            {
                var user = await usersService.Auth(context);
                await usersService.RegisterUserToEvent(request.idEvent, user.Id);

                return Results.Ok("Пользователь зарегистрирован на событие");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> GetMyEvents(UsersService usersService, HttpContext context)
        {
            try
            {
                var user = await usersService.Auth(context);
                var myEvents = (await usersService.GetMyEvents(user)).Select(e => new EventResponse(e.Id, e.Title, e.Description, e.Date, e.Location, e.Category, e.MaxUserCount, e.Images.Select(i => new ImageResponse(i.Id, i.EventId, i.Link)).ToList(), e.Users.Select(u => new UserResponse(u.Id)).ToList()));
                return Results.Ok(myEvents);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> GetUsersByEvent(GetUsersByEventRequest request, UsersService usersService)
        {
            try
            {
                var users = await usersService.GetUsersByEvent(request.idEvent);

                return Results.Ok(users);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> GetUserById(GetUserByIdRequest request, UsersService usersService)
        {
            try
            {
                var user = await usersService.GetUserById(request.idUser);

                return Results.Ok(user);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }

        public static async Task<IResult> DeleteUserInEvent(DeleteUserInEventRequest request, UsersService usersService)
        {
            try
            {
                await usersService.DeleteUserInEvent(request.idUser, request.idEvent);

                return Results.Ok("Пользователь удален");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Massage = ex.Message });
            }
        }
    }
}
