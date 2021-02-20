using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Site.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetAuthToken(this HttpContext context)
            => context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        public static string GetRequestUrl(this HttpContext context)
        {
            var req = context.Request;
            return $"{req.Scheme}://{req.Host}{req.Path}{req.QueryString}";
        }

        public static string GetUserName(this HttpContext context) => context?.User.Identity?.Name;

        public static string GetRemoteIpAddress(this HttpContext context) =>
            context?.Connection.RemoteIpAddress?.ToString();

        public static string GetSessionId(this HttpContext context) => context?.Session.Id;
        public static string GetUserAgent(this HttpContext context) => context.Request.Headers["User-Agent"].ToString();

        public static IEnumerable<KeyValuePair<string, string>> GetHeaders(this HttpContext context)
        {
            var headers = new List<KeyValuePair<string,string>>();
            foreach (var (name, stringValues) in context.Request.Headers)
            {
                var key = $"Headers.{name.TrimStart(':')}";
                var value = string.Join(",", stringValues.ToArray());
                headers.Add(new KeyValuePair<string, string>(key, value));
            }
            return headers;
        }
    }
}
