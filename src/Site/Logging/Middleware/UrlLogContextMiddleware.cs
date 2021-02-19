using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Site.Logging.Middleware
{
    public class UrlLogContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UrlLogContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var req = context.Request;
            LogContext.PushProperty("Url", $"{req.Scheme}://{req.Host}{req.Path}{req.QueryString}");
            return _next(context);
        }
    }
}