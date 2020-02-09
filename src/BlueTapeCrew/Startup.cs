using System.Reflection;
using AutoMapper;
using BlueTapeCrew.Identity;
using BlueTapeCrew.Mappings;
using BlueTapeCrew.Services;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Extensions;

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
            var defaultConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<BtcEntities>(options => options.UseSqlServer(defaultConnectionString));
            services.AddDbContext<IdentityEntities>(options => options.UseSqlServer(defaultConnectionString));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityEntities>();

            services.AddControllersWithViews();
            services.ConfigureApplicationCookie(options =>{  options.LoginPath = "/Account/Login"; });

            //configure service layer middleware returns AutoMapper mappings
            //haven't found a great way to add mappings from multiple assemblies yet
            //todo: find a more elegant way to do this
            var serviceMappings = services.AddServiceLayer();
            var webMappings = Assembly.GetAssembly(typeof(WebMappings));
            services.AddAutoMapper(webMappings, serviceMappings);

            RegisterWebServices(services);

        }

        public static void RegisterWebServices(IServiceCollection services)
        {
            services.AddTransient<ICookieService, CookieService>();
            services.AddTransient<ISessionService, SessionService>();
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