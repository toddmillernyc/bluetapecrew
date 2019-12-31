using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Mappings;
using System.Reflection;
using Repositories.Extensions;

namespace Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.UseRepositoryLayer();
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ServiceMappings)));
            return RegisterTypes(services);
        }

        private static IServiceCollection RegisterTypes(IServiceCollection services)
        {
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<ICartCalculatorService, CartCalculatorService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICheckoutService, CheckoutService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailSubscriptionService, EmailSubscriptionService>();
            services.AddTransient<IGuestUserService, GuestUserService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaypalService, PaypalService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IShippingService, ShippingService>();
            services.AddTransient<ISiteSettingsService, SiteSettingsService>();
            services.AddTransient<IStyleService, StyleService>();
            return services;
        }
    }
}
