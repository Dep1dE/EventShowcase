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
    public class RolePermissionConfiguration: IEntityTypeConfiguration<RolePermission>
    {
        private readonly AuthorizationOptions _authorizationOptions;

        public RolePermissionConfiguration(AuthorizationOptions authorization)
        {
            _authorizationOptions = authorization;
        }

        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(r => new {r.RoleId, r.PermissionId});
            var e = ParseRolepermissions();
            builder.HasData(ParseRolepermissions());
        }

        private RolePermission[] ParseRolepermissions()
        {
            return _authorizationOptions.RolePermissions
                .SelectMany(rp => rp.Permissions.Select(p => new RolePermission
                    {
                        RoleId = (int)Enum.Parse<UserRoles>(rp.Role),
                        PermissionId = (int)Enum.Parse<UserPermissions>(p)
                    })
                )
                .ToArray();
        }
    }
}
