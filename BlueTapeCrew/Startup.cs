using AutoMapper;
using BlueTapeCrew.Identity;
using BlueTapeCrew.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories;
using Repositories.Entities;
using Repositories.Interfaces;
using Services;
using Services.Interfaces;

namespace BlueTapeCrew
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { Configuration = configuration; }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            var defaultConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<BtcEntities>(options => options.UseSqlServer(defaultConnectionString));
            services.AddDbContext<IdentityEntities>(options => options.UseSqlServer(defaultConnectionString));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityEntities>();
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
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<IGuestUserRepository, GuestUserRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductCategoriesRepository, ProductCategoriesRepository>();
            services.AddTransient<IProductImageRepository, ProductImageRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<ISiteSettingsRepository, SiteSettingsRepository>();
            services.AddTransient<ISizeRepository, SizeRepository>();
            services.AddTransient<IStyleRepository, StyleRepository>();
        }

        public static void RegisterServiceTypes(IServiceCollection services)
        {
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<ICartCalculatorService, CartCalculatorService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICheckoutService, CheckoutService>();
            services.AddTransient<ICookieService, CookieService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailSubscriptionService, EmailSubscriptionService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaypalService, PaypalService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IShippingService, ShippingService>();
            services.AddTransient<ISiteSettingsService, SiteSettingsService>();
            services.AddTransient<IStyleService, StyleService>();
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