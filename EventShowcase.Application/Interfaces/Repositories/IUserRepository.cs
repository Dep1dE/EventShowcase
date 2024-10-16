using EventShowcase.Core.Enums;
using EventShowcase.Core.Models;

namespace EventShowcase.Application.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task RegisterUserToEventAsync(Guid idEvent, Guid idUser);
        Task<User?> GetUserWhihtEventsAsync(Guid id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<List<User>> GetUsersByEventAsync(Guid idEvent);
        Task DeleteUserInEventAsync(Guid idUser, Guid idEvent);
        Task<HashSet<UserPermissions>> GetUserPermissions(Guid idUser);
    }
}
