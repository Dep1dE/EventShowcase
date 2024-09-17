using EventShowcase.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.Interfaces.Services
{
    public interface IPermissionService
    {
        Task<HashSet<UserPermissions>> GetPermissionsAsync(Guid idUser);
    }
}
