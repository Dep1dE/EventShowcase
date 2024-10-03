using EventShowcase.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.Interfaces.Services
{
    public interface IUsersService
    {
        Task<User> Auth(string token);
    }
}
