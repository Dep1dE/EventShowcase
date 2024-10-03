using EventShowcase.Core.Enums;
using EventShowcase.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace EventShowcase.API.Extensions
{
    public static class AuthorizationExtensions
    {
        public static AuthorizationPolicyBuilder RequirePermissions(
            this AuthorizationPolicyBuilder builder, params UserPermissions[] permissions)
        {
            return builder.AddRequirements(new PermissionRequirement(permissions));
        }
    }   
}
