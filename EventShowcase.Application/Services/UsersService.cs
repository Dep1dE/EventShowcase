using EventShowcase.Application.Interfaces.Auth;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Application.Interfaces.Services;
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
    public class UsersService: IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IHttpContextAccessor httpContextAccessor) 
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<User> Auth(string token)
        {
            try
            {
                var userId = _jwtProvider.GetUserIdFromToken(token);

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
                try
                {
                    var newAccessToken = _jwtProvider.RefreshAccessToken(token);

                    try
                    {
                        var userId = _jwtProvider.GetUserIdFromToken(newAccessToken);

                        try
                        {
                            var user = await _userRepository.GetUserByIdAsync(userId);
                            return user;
                        }
                        catch (Exception ex1)
                        {
                            throw new Exception(ex1.Message);
                        }
                    }
                    catch (Exception ex2)
                    {
                        throw new Exception(ex2.Message);
                    }
                }
                catch (Exception ex3)
                {
                    throw new Exception(ex3.Message);
                }
                throw new Exception(ex.Message);
            }
        }
    }
}
