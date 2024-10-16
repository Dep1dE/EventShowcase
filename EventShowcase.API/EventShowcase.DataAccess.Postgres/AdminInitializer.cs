using EventShowcase.Core.Enums;
using EventShowcase.Core.Models;
using EventShowcase.DataAccess.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EventShowcase.Infrastructure
{
    public class AdminInitializer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        public AdminInitializer(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public async Task EnsureAdminUserExistsAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EventShowcaseDbContext>();
                var passwordHasher = new PasswordHasher();

                if (!await dbContext.Users.AnyAsync(u => u.Name == "admin"))
                {
                    var adminRole = await dbContext.Roles.SingleOrDefaultAsync(r => r.Id == (int)UserRoles.Admin);
                    var adminUser = new User
                    {
                        Name = _configuration["AdminUser:Name"],
                        Email = _configuration["AdminUser:Email"],
                        PasswordHash = passwordHasher.Generate(_configuration["AdminUser:Password"]),
                        IsAdmin = true,
                    };

                    var roleEntity = await dbContext.Roles.SingleOrDefaultAsync(
                    r => r.Id == (int)UserRoles.Admin);

                    adminUser.Roles.Add(roleEntity);

                    await dbContext.Users.AddAsync(adminUser);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
