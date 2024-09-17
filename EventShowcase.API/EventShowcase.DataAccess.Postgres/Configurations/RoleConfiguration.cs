using EventShowcase.Core.Enums;
using EventShowcase.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Role = EventShowcase.Core.Models.Role;

namespace EventShowcase.DataAccess.Postgres.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Core.Models.Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermission>(
                    l => l.HasOne<Core.Models.Permission>().WithMany().HasForeignKey(e => e.PermissionId),
                    r => r.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleId)
                );

            var roles = Enum
                .GetValues<Core.Enums.UserRoles>()
                .Select(r => new Role
                {
                    Id = (int)r,
                    Name = r.ToString(),
                });
            
            builder.HasData(roles);
        }
    }
}
