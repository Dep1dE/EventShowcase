using EventShowcase.Application.Interfaces.Auth;
using EventShowcase.Application.Interfaces.Services;
using EventShowcase.Application.Services;
using EventShowcase.Core.Enums;
using EventShowcase.Core.Models;
using EventShowcase.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EventShowcase.API.Extensions
{
    public static class ApiExtensions
    {
        public static void AddApiAuthentication(
            this IServiceCollection services,
            IConfiguration configuration) //configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["tasty-cookies"];

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddScoped<IPermissionService, PermissionService>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddAuthorization();
        }

        public static IEndpointConventionBuilder RequirePermissions<TBuilder>(
            this TBuilder builder, params UserPermissions[] permissions)
                where TBuilder : IEndpointConventionBuilder
        {
            return builder.RequireAuthorization(policy =>
                policy.AddRequirements(new PermissionRequirement(permissions)));
        }



    }
}
