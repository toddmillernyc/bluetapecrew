using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Services.Extensions;
using Services.Models;

namespace Site.Security.Jwt
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private static readonly ILogger Logger = new LoggerFactory().CreateLogger<JwtAuthorizeAttribute>();

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var user = (User) context.HttpContext.Items["User"];
                if (user == null)
                {
                    context.Result = new JsonResult(new {message = "Unauthorized"})
                        {StatusCode = StatusCodes.Status401Unauthorized};
                }
            }
            catch (Exception ex)
            {
                ex = ex.ToInner();
                Logger.LogError(ex, ex.Message);
            }
        }
    }
}
