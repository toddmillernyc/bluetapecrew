using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Services.Extensions;
using Site.Mappings;
using Site.Services;

namespace Site.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAndConfigureSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            return services;
        }

        //configure service layer middleware returns AutoMapper mappings
        //haven't found a great way to add mappings from multiple assemblies yet
        //todo: find a more elegant way to do this
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            var serviceMappings = services.AddServiceLayer();
            var webMappings = Assembly.GetAssembly(typeof(WebMappings));
            services.AddAutoMapper(webMappings, serviceMappings);
            return services;
        }

        public static IServiceCollection RegisterWebServices(this IServiceCollection services)
        {
            services.AddTransient<ICookieService, CookieService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IUserRegistrationService, UserRegistrationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IViewModelService, ViewModelService>();
            return services;
        }
    }
}
