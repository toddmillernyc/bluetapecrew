using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Site.Logging.Middleware
{
    public class IpAddressLogContextMiddleware
    {
        private readonly RequestDelegate _next;

        public IpAddressLogContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var ipAddress = context?.Connection.RemoteIpAddress?.ToString();
            if (!string.IsNullOrEmpty(ipAddress))
                LogContext.PushProperty("IpAddress", ipAddress);
            return _next(context);
        }
    }
}