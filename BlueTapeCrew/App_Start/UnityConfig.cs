using System;
using BlueTapeCrew.Interfaces;
using Unity;
using BlueTapeCrew.Services;
using Microsoft.AspNet.Identity;
using BlueTapeCrew.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using BlueTapeCrew.Controllers;
using Microsoft.Owin.Security;
using Unity.Injection;
using Unity.Lifetime;
using System.Web;
using BlueTapeCrew.Paypal;
using BlueTapeCrew.Repositories;

namespace BlueTapeCrew
{
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer Container => container.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            RegisterIdentityTypes(container);
            RegisterRepositoryTypes(container);
            RegisterServiceTypes(container);

        }

        private static void RegisterIdentityTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));
        }

        private static void RegisterRepositoryTypes(IUnityContainer container)
        {
            container.RegisterType<IAccessTokenRepository, AccessTokenRepository>();
            container.RegisterType<ISettingsRepository, SettingsRepository>();
        }

        private static void RegisterServiceTypes(IUnityContainer container)
        {
            container.RegisterType<ICartService, CartService>();
            container.RegisterType<ICookieService, CookieService>();
            container.RegisterType<IEmailSubscriptionService, EmailSubscriptionService>();
            container.RegisterType<IImageService, ImageService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IPaypalService, PaypalService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<ISiteSettingsService, SiteSettingsService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IViewModelService, ViewModelService>();
            container.RegisterType<IWebService, WebService>();
        }
    }
}