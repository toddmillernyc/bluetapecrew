using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;

namespace Repositories.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseRepositoryLayer(this IServiceCollection services)
        {

            return RegisterTypes(services);
        }

        private static IServiceCollection RegisterTypes(IServiceCollection services)
        {
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<IGuestUserRepository, GuestUserRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductCategoriesRepository, ProductCategoriesRepository>();
            services.AddTransient<IProductImageRepository, ProductImageRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<ISiteProfileRepository, SiteProfileRepository>();
            services.AddTransient<ISiteSettingsRepository, SiteSettingsRepository>();
            services.AddTransient<ISizeRepository, SizeRepository>();
            services.AddTransient<IStyleRepository, StyleRepository>();
            return services;
        }
    }
}
