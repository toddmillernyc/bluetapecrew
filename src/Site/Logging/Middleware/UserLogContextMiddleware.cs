using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Site.Logging.Middleware
{
    public class UserLogContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserLogContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var username = context?.User.Identity?.Name;
            if (!string.IsNullOrEmpty(username)) 
                LogContext.PushProperty("UserName", username);
            return _next(context);
        }
    }
}
