namespace Site.Logging
{
    public static class LoggingConstants
    {
        public static readonly string[] ErrorContexts =
        {
            "Microsoft.EntityFrameworkCore.Query"
        };

        public static readonly string[] WarningContexts = {
            "Microsoft.AspNetCore.Hosting.Diagnostics",
            "Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker",
            "Microsoft.AspNetCore.Mvc.Infrastructure.ObjectResultExecutor",
            "Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware",
            "Microsoft.AspNetCore.Mvc.ViewFeatures.ViewResultExecutor",
            "Microsoft.AspNetCore.Routing.EndpointMiddleware",
            "Microsoft.AspNetCore.Session.DistributedSession",
            "Microsoft.EntityFrameworkCore.Database.Command",
            "Microsoft.EntityFrameworkCore.Infrastructure"
        };
    }
}
