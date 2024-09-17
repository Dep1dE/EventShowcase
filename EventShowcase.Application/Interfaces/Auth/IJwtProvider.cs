using EventShowcase.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.Interfaces.Auth
{
    public interface IJwtProvider
    {
        public string GenerateAccessToken(User user);
        public Guid GetUserIdFromToken(IRequestCookieCollection cookies);
    }
}
