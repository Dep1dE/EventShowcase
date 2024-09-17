using EventShowcase.Core.Enums;
using EventShowcase.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.DataAccess.Postgres.Configurations
{
    public class PermissionConfiguration: IEntityTypeConfiguration<Core.Models.Permission>
    {
        public void Configure(EntityTypeBuilder<Core.Models.Permission> builder)
        {
            builder.HasKey(p => p.Id);

            var permissions = Enum
                .GetValues<Core.Enums.UserPermissions>()
                .Select(p => new Core.Models.Permission
                {
                    Id = (int)p,
                    Name = p.ToString()
                });

            builder.HasData(permissions);
        }
    }
}
