using EventShowcase.Core.Models;
using EventShowcase.DataAccess.Postgres.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EventShowcase.DataAccess.Postgres
{
    public class EventShowcaseDbContext : DbContext
    {
        private readonly AuthorizationOptions _authOptions;

        public EventShowcaseDbContext(DbContextOptions<EventShowcaseDbContext> options,
            IOptions<AuthorizationOptions> authOptions) : base(options)
        {
            _authOptions = authOptions.Value;
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(_authOptions));

            base.OnModelCreating(modelBuilder);
        }
    }
}
