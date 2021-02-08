using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Site.Extensions
{
    public static class HttpContextExtensions
    {
        public static string ToAuthToken(this HttpContext context)
            => context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
    }
}
