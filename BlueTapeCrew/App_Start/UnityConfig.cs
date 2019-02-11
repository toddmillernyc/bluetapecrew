using BlueTapeCrew.Controllers;
using BlueTapeCrew.Identity;
using BlueTapeCrew.Repositories;
using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services;
using BlueTapeCrew.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Data.Entity;
using System.Web;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

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
            container.RegisterType<ICategoryProductsRepository, CategoryProductsRepository>();
            container.RegisterType<IInvoiceRepository, InvoiceRepository>();
            container.RegisterType<IMenuService, MenuService>();
            container.RegisterType<ICartRepository, CartRepository>();
            container.RegisterType<ISiteSettingsRepository, SiteSettingsRepository>();
        }

        private static void RegisterServiceTypes(IUnityContainer container)
        {
            container.RegisterType<ICartService, CartService>();
            container.RegisterType<ICartCalculatorService, CartCalculatorService>();
            container.RegisterType<ICheckoutService, CheckoutService>();
            container.RegisterType<ICookieService, CookieService>();
            container.RegisterType<IEmailService, Services.EmailService>();
            container.RegisterType<IEmailSubscriptionService, EmailSubscriptionService>();
            container.RegisterType<IImageService, ImageService>();
            container.RegisterType<IInvoiceService, InvoiceService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IPaypalService, PaypalService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IShippingService, ShippingService>();
            container.RegisterType<ISiteSettingsService, SiteSettingsService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IViewModelService, ViewModelService>();
        }
    }
}