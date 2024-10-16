using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Enums;
using EventShowcase.Core.Models;
using EventShowcase.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventShowcase.DataAccess.Postgres.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        private readonly PasswordHasher checkPassword = new PasswordHasher();
        public UserRepository(EventShowcaseDbContext dbContext) : base(dbContext) { }

        public async Task RegisterUserToEventAsync(Guid idEvent, Guid idUser)
        {
            var Event = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == idEvent);
            var User = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == idUser);

            Event.Users.Add(User);

            await _dbContext.SaveChangesAsync();
        }
        public async Task<User?> GetUserWhihtEventsAsync(Guid id)
        {
            return await _dbContext.Users.AsNoTracking().Include(e => e.Events).FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task AddAsync(User user)
        {
            var roleEntity = await _dbContext.Roles.SingleOrDefaultAsync(
                    r => r.Id == (int)UserRoles.User);
            user.Roles.Add(roleEntity);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<User>> GetUsersByEventAsync(Guid idEvent)
        {
            var Event = await _dbContext
                .Events.AsNoTracking()
                .Include(u => u.Users).FirstOrDefaultAsync(x => x.Id == idEvent);
            return Event.Users;
        }

        public async Task DeleteUserInEventAsync(Guid idUser, Guid idEvent)
        {
            var eventEntity = await _dbContext
                .Events
                .Include(u => u.Users)
                .FirstOrDefaultAsync(x => x.Id == idEvent);

            var userToRemove = eventEntity.Users.FirstOrDefault(x => x.Id == idUser);

            eventEntity.Users.Remove(userToRemove);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<HashSet<UserPermissions>> GetUserPermissions(Guid idUser)
        {
            var roles = await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(r => r.Permissions)
                .Where(u => u.Id == idUser)
                .Select(u => u.Roles)
                .ToListAsync();

            return roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(p => (UserPermissions)p.Id)
                .ToHashSet();
        }


    }
}
