using EventShowcase.Application.Interfaces.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Infrastructure
{
    public class JwtOptions: IJwtOptions
    {
        public JwtOptions() { }
        public JwtOptions(IConfiguration config)
        {
            SecretKey = config["JwtOptions:SecretKey"]!;
            ExpireHours = Convert.ToInt32(config["JwtOptions:ExpiresHours"]!);
        }
        public string SecretKey { get; set; } = string.Empty;
        public int ExpireHours { get; set; } //Minutes
    }
}

