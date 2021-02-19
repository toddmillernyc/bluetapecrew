using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Site.Logging.Middleware;

namespace Site.Logging
{
    public static class LoggingExtensions
    {

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
            foreach (var context in LoggingConstants.WarningContexts)
                loggerConfiguration.MinimumLevel.Override(context, Serilog.Events.LogEventLevel.Warning);
            foreach (var context in LoggingConstants.ErrorContexts)
                loggerConfiguration.MinimumLevel.Override(context, Serilog.Events.LogEventLevel.Error);
            return loggerConfiguration;
        }
    }
}
