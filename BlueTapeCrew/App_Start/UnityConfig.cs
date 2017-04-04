using System;
using System.Data.Entity;
using BlueTapeCrew.Controllers;
using BlueTapeCrew.Identity;
using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;

namespace BlueTapeCrew
{
    public class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            //types for default identity config
            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            //project specific types
            container.RegisterType<ICartService, CartService>();
            container.RegisterType<IImageService, ImageService>();
            container.RegisterType<IViewModelService, ViewModelService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<ICheckoutService, CheckoutService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IEmailSubscriptionService,EmailSubscriptionService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<ICookieService, CookieService>();
            container.RegisterType<ISiteSettingsService, SiteSettingsService>();
            container.RegisterType<IPaypalService, PaypalService>();
        }
    }
}
