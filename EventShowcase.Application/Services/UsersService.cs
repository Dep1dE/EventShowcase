using EventShowcase.Application.Interfaces.Auth;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Models;
using EventShowcase.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.Services
{
    public class UsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UsersService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider) 
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }


        public async Task<User> Auth(HttpContext httpContext)
        {
            try
            {
                var allCookies = httpContext.Request.Cookies;

                var userId = _jwtProvider.GetUserIdFromToken(allCookies);

                try
                {
                    var user = await _userRepository.GetUserByIdAsync(userId);
                    return user;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Register(string userName, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = new User
            {
                Id = new Guid(),
                Name = userName,
                Email = email,
                PasswordHash = hashedPassword
            };
            await _userRepository.AddUserAsync(user);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            
            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if(result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.GenerateAccessToken(user);

            return token;
        }

        public async Task RegisterUserToEvent(Guid idEvent, Guid idUser)
        {
            await _userRepository.RegisterUserToEventAsync(idEvent, idUser);
        }

        public async Task<List<Event>> GetMyEvents(User user)
        {
            var userWhithEvents = await _userRepository.GetUserWhihtEventsAsync(user.Id);
            return userWhithEvents.Events;
        }

        public async Task<List<User>> GetUsersByEvent(Guid idEvent)
        {
            return await _userRepository.GetUsersByEventAsync(idEvent);
        }

        public async Task<User> GetUserById(Guid idUser)
        {
            return await _userRepository.GetUserByIdAsync(idUser);
        }

        public async Task DeleteUserInEvent(Guid idUser, Guid idEvent)
        {
            await _userRepository.DeleteUserInEventAsync(idUser, idEvent);
        }
    }
}
