using EventShowcase.Core.Enums;
using EventShowcase.Core.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Infrastructure
{
    public class PermissionRequirement: IAuthorizationRequirement
    {
        public PermissionRequirement(UserPermissions[] permissions) 
        {
            Permissions = permissions;   
        }
        public UserPermissions[] Permissions { get; set; } = [];
    }
}
