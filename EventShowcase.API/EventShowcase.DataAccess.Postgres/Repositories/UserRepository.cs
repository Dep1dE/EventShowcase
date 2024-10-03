using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Core.Enums;
using EventShowcase.Core.Models;
using EventShowcase.Core.Validators;
using EventShowcase.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventShowcase.DataAccess.Postgres.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly EventShowcaseDbContext _dbContext;
        private readonly IValidator<User> _validator;
        private readonly PasswordHasher checkPassword = new PasswordHasher();
        public UserRepository(EventShowcaseDbContext dbContext, IValidator<User> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task RegisterUserToEventAsync(Guid idEvent, Guid idUser)
        {
            var Event =
                await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == idEvent)
                ?? throw new Exception("Событие не найдено");
            var User = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == idUser)
                ?? throw new Exception("Пользователь не найден");

            Event.Users.Add(User);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) ?? 
                throw new Exception("Пользователь не найден");

        }

        public async Task<User> GetUserWhihtEventsAsync(Guid id)
        {
            return await _dbContext.Users.AsNoTracking().Include(e => e.Events).FirstOrDefaultAsync(x => x.Id == id) ??
                throw new Exception("Пользователь не найден");
        }

        public async Task AddUserAsync(User user)
        {
            var roleEntity = await _dbContext.Roles.SingleOrDefaultAsync(
                    r => r.Id == (int)UserRoles.User);

            if (user.Name == "admin" && checkPassword.Verify("admin", user.PasswordHash))
            { 
                roleEntity = await _dbContext.Roles.SingleOrDefaultAsync(
                    r => r.Id == (int)UserRoles.Admin); 
            }
           
            user.Roles.Add(roleEntity);

            if(roleEntity.Name == "Admin")
            {
                user.IsAdmin = true;
            }

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email) ?? throw new Exception()    ;
        }

        public async Task<List<User>> GetUsersByEventAsync(Guid idEvent)
        {
            var Event = await _dbContext
                .Events.AsNoTracking()
                .Include(u => u.Users).FirstOrDefaultAsync(x => x.Id == idEvent) ?? throw new Exception("Пользователи не найдены");
            return Event.Users;
        }

        public async Task DeleteUserInEventAsync(Guid idUser, Guid idEvent)
        {
            var eventEntity = await _dbContext
                .Events
                .Include(u => u.Users)
                .FirstOrDefaultAsync(x => x.Id == idEvent) ?? throw new Exception("Событие не найдено");

            var userToRemove = eventEntity.Users.FirstOrDefault(x => x.Id == idUser)
                                ?? throw new Exception("Пользователь не найден");

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
