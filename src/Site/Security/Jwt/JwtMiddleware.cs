using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Extensions;
using Site.Extensions;
using Site.Services.Interfaces;

namespace Site.Security.Jwt
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _appSettings;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> appSettings, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            try
            {
                var token = context.GetAuthToken();
                if (token != null)
                    AttachUserToContext(context, userService, token);
            }
            catch (Exception ex)
            {
                ex = ex.ToInner();
                _logger.LogError(ex, ex.Message);
            }
            finally
            {
                await _next(context);
            }
        }

        private void AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero //default 5 min
                }, out var validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
                context.Items["User"] = userService.Find(userId);
            }
            catch(Exception ex)
            {
                ex = ex.ToInner();
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}