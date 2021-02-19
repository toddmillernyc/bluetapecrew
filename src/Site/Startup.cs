using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Site.Extensions;
using Site.Logging;
using Site.Security.Identity;
using Site.Security.Jwt;

namespace Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { Configuration = configuration; }
        public IConfiguration Configuration { get; }

        private string ConnectionString => Configuration.GetConnectionString("DefaultConnection");

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJwt(Configuration);
            services
                .AddCors()
                .AddDistributedMemoryCache()
                .AddAndConfigureSession()
                .AddDbContext<BtcEntities>(options => options.UseSqlServer(ConnectionString))
                .AddIdentity(ConnectionString)
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services
                .RegisterAutoMapper()
                .RegisterWebServices();
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
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseLogging();
            app.UseMiddleware<JwtMiddleware>();
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