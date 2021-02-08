using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Site.Security.Identity
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, string connectionString)
        {
            services
                .AddDbContext<IdentityEntities>(options => options.UseSqlServer(connectionString))
                .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityEntities>();
            services.ConfigureApplicationCookie(options => { options.LoginPath = "/Account/Login"; });
            return services;
        }
    }
}
