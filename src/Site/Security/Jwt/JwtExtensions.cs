using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Site.Security.Jwt.Interfaces;

namespace Site.Security.Jwt
{
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            return services;
        }
    }
}
