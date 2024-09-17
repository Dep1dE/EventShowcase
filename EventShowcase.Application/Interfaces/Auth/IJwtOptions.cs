using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.Interfaces.Auth
{
    public interface IJwtOptions
    {
        int ExpireHours { get; set; }
        string SecretKey { get; set; }
    }
}
