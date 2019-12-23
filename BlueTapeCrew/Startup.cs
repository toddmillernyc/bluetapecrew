using BlueTapeCrew.Data;
using BlueTapeCrew.Repositories;
using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services;
using BlueTapeCrew.Services.Interfaces;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IUserRegistrationService = BlueTapeCrew.Services.Interfaces.IUserRegistrationService;

namespace BlueTapeCrew
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { Configuration = configuration; }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddDbContext<BtcEntities>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BtcEntities>();
            services.AddControllersWithViews();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });
            RegisterRepositoryTypes(services);
            RegisterServiceTypes(services);
        }

        public static void RegisterRepositoryTypes(IServiceCollection services)
        {
            services.AddTransient<ICategoryProductsRepository, CategoryProductsRepository>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ISiteSettingsRepository, SiteSettingsRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
        }

        public static void RegisterServiceTypes(IServiceCollection services)
        {
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<ICartCalculatorService, CartCalculatorService>();
            services.AddTransient<ICheckoutService, CheckoutService>();
            services.AddTransient<ICookieService, CookieService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailSubscriptionService, EmailSubscriptionService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<INewSiteService, NewSiteService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaypalService, PaypalService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IShippingService, ShippingService>();
            services.AddTransient<ISiteSettingsService, SiteSettingsService>();
            services.AddTransient<IUserRegistrationService, UserRegistrationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IViewModelService, ViewModelService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (Configuration.GetValue<bool>("UseDeveloperExceptionPage")) app.UseDeveloperExceptionPage();
            else app.UseExceptionHandler("/Home/Error");

            if (Configuration.GetValue<bool>("UseDatabaseErrorPage")) app.UseDeveloperExceptionPage();

            if (!env.IsDevelopment()) app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {


                endpoints.MapControllers();
                endpoints.MapAreaControllerRoute(
                    "admin",
                    "admin",
                    "Admin/{controller=AdminProducts}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}