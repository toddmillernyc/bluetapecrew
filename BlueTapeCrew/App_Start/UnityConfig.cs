using System;
using BlueTapeCrew.Interfaces;
using Unity;
using BlueTapeCrew.Services;
using Microsoft.AspNet.Identity;
using BlueTapeCrew.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Net;
using BlueTapeCrew.Controllers;
using Microsoft.Owin.Security;
using Unity.Injection;
using Unity.Lifetime;
using System.Web;

namespace BlueTapeCrew
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(
                    o => System.Web.HttpContext.Current.GetOwinContext().Authentication
                )
            );
            container.RegisterType<ICartService, CartService>();
            container.RegisterType<ICheckoutService, CheckoutService>();
            container.RegisterType<ICookieService, CookieService>();
            container.RegisterType<IEmailSubscriptionService, EmailSubscriptionService>();
            container.RegisterType<IImageService, ImageService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IPaypalService, PaypalService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<ISiteSettingsService, SiteSettingsService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IViewModelService, ViewModelService>();
            
        }
    }
}