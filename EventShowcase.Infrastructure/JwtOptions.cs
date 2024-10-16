using EventShowcase.Application.Interfaces.Auth;
using Microsoft.Extensions.Configuration;

namespace EventShowcase.Infrastructure
{
    public class JwtOptions: IJwtOptions
    {
        public JwtOptions() { }
        public JwtOptions(IConfiguration config)
        {
            SecretKey = config["JwtOptions:SecretKey"]!;
            ExpireHoursAccess = Convert.ToInt32(config["JwtOptions:ExpiresHoursAccess"]!);
            ExpireHoursRefresh = Convert.ToInt32(config["JwtOptions:ExpiresHoursRefresh"]!);
        }
        public string SecretKey { get; set; } = string.Empty;
        public int ExpireHoursAccess { get; set; } //Minutes
        public int ExpireHoursRefresh { get; set; } //Minutes
    }
}

