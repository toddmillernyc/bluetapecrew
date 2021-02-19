using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Site.Logging.Middleware;

namespace Site.Logging
{
    public static class LoggingExtensions
    {
        private static readonly string[] WarningContexts = {
            "Microsoft.AspNetCore.Hosting.Diagnostics",
            "Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker",
            "Microsoft.AspNetCore.Mvc.Infrastructure.ObjectResultExecutor",
            "Microsoft.AspNetCore.Mvc.ViewFeatures.ViewResultExecutor",
            "Microsoft.AspNetCore.Routing.EndpointMiddleware",
            "Microsoft.AspNetCore.Session.DistributedSession",
            "Microsoft.EntityFrameworkCore.Database.Command",
            "Microsoft.EntityFrameworkCore.Infrastructure"
        };

        private static readonly string[] ErrorContexts =
        {
            "Microsoft.EntityFrameworkCore.Query"
        };

        public static IWebHostBuilder UseLogging(this IWebHostBuilder webBuilder)
        {
            webBuilder
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .ApplyOverrides()
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                        .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment);
                });
            return webBuilder;
        }

        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();
            app.UseMiddleware<UserLogContextMiddleware>();
            app.UseMiddleware<IpAddressLogContextMiddleware>();
            app.UseMiddleware<SessionIdLogContextMiddleware>();
            return app;
        }

        private static LoggerConfiguration ApplyOverrides(this LoggerConfiguration loggerConfiguration)
        {
            foreach (var context in WarningContexts)
                loggerConfiguration.MinimumLevel.Override(context, Serilog.Events.LogEventLevel.Warning);

            foreach (var context in ErrorContexts)
                loggerConfiguration.MinimumLevel.Override(context, Serilog.Events.LogEventLevel.Error);

            return loggerConfiguration;
        }
    }
}
