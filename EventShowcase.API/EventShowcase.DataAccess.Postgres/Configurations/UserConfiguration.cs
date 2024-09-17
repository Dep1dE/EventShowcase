using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventShowcase.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventShowcase.DataAccess.Postgres.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasMany(e => e.Events).WithMany(u => u.Users);
            builder.HasMany(u => u.Roles).WithMany(r => r.Users)
                .UsingEntity<UserRole>(
                    l => l.HasOne<Role>().WithMany().HasForeignKey(r => r.RoleId),
                    r => r.HasOne<User>().WithMany().HasForeignKey(u => u.UserId)
                );
        }
    }
}
