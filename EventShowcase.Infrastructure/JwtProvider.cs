using EventShowcase.Application.Interfaces.Auth;
using EventShowcase.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Infrastructure
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public string GenerateAccessToken(User user)
        {
            Claim[] claims = [new("userId", user.Id.ToString())];

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpireHoursAccess));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public string GenerateRefreshToken(User user)
        {
            Claim[] claims = [new("userId", user.Id.ToString())];

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpireHoursRefresh));

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public Guid GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.SecretKey);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.FromMinutes(0)
                }, out SecurityToken validatedToken);

                var userIdClaim = principal.FindFirst("userId");
                if (userIdClaim != null)
                {
                    return Guid.Parse(userIdClaim.Value);
                }
            }
            catch (SecurityTokenExpiredException)
            {

                Console.WriteLine("Токен истек.");
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                Console.WriteLine("Подпись токена недействительна.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при валидации токена: {ex.Message}");
            }

            throw new Exception("Пользователь не найден");

        }

        public string RefreshAccessToken(string refreshToken)
        {
            try { 
                var user = new User { Id = GetUserIdFromToken(refreshToken) }; 
                var newAccessToken = GenerateAccessToken(user);
                var newRefreshToken = GenerateRefreshToken(user);
                return newAccessToken;
            }
            catch {
                throw new SecurityTokenException("Недействительный Refresh Token."); 
            }
        }
    }
}


