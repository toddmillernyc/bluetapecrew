using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Site.Logging.Middleware
{
    public class SessionIdLogContextMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionIdLogContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var sessionId = context?.Session.Id;
            if (!string.IsNullOrEmpty(sessionId))
                LogContext.PushProperty("SessionId", sessionId);
            return _next(context);
        }
    }
}