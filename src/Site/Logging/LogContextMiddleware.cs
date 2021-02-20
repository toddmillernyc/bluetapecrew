using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using Site.Extensions;

namespace Site.Logging
{
    public class LogContextMiddleware
    {
        private readonly RequestDelegate _next;

        public LogContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            EnrichContext("GetSessionId", context.GetSessionId());
            EnrichContext("IpAddress", context.GetRemoteIpAddress());
            EnrichContext("Url", context.GetRequestUrl());
            EnrichContext("UserAgent", context.GetUserAgent());
            EnrichContext("UserName", context.GetUserName());
            return _next(context);
        }

        private static void EnrichContext(string key, string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            LogContext.PushProperty(key, value);
        }
    }
}
