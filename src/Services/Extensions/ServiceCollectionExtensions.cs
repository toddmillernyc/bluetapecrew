using Microsoft.Extensions.DependencyInjection;
using Repositories.Extensions;
using Services.Interfaces;
using Services.Mappings;
using System.Reflection;

namespace Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static Assembly AddServiceLayer(this IServiceCollection services)
        {
            services.UseRepositoryLayer();
            RegisterTypes(services);
            var autoMapperAssembly = Assembly.GetAssembly(typeof(ServiceMappings));
            return autoMapperAssembly;
        }

        private static void RegisterTypes(IServiceCollection services)
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
            services.AddTransient<IViewModelService, ViewModelService>();
        }
    }
}
