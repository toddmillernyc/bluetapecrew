using Entities;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using React.Site.GraphQL;

namespace React.Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { Configuration = configuration; }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            services.AddDbContext<BtcEntities>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddGraphQL(
                SchemaBuilder.New()
                    .AddQueryType<Query>()
                    .Create(),
                new QueryExecutionOptions { ForceSerialExecution = true });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseCors();
            }
            app.UseGraphQL();
            if (env.IsDevelopment())
            {
                app.UsePlayground();
            }
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
