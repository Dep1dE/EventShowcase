using EventShowcase.Core.Enums;
using EventShowcase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task RegisterUserToEventAsync(Guid idEvent, Guid idUser);
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserWhihtEventsAsync(Guid id);
        Task AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetUsersByEventAsync(Guid idEvent);
        Task DeleteUserInEventAsync(Guid idUser, Guid idEvent);
        Task<HashSet<UserPermissions>> GetUserPermissions(Guid idUser);
    }
}
