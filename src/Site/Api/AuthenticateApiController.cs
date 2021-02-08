using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Extensions;
using Site.Models;
using Site.Security.Identity;
using Site.Security.Jwt.Interfaces;
using Site.Services;

namespace Site.Api
{
    [ApiController]
    [Route("api/authenticate")]
    public class AuthenticateApiController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticateApiController> _logger;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticateApiController(
            IUserService userService,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenService jwtTokenService,
            ILogger<AuthenticateApiController> logger)
        {
            _userService = userService;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AuthenticationRequest request)
        {
            try
            {
                var token = await Authenticate(request.Username, request.Password);
                return token == null
                    ? BadRequest(new { message = "Username or password is incorrect" })
                    : Ok(new { token });
            }
            catch (Exception ex)
            {
                ex = ex.ToInner();
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        private async Task<string> Authenticate(string username, string password)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(username, password, false, false);
            if (!loginResult.Succeeded)
            {
                _logger.LogError("Bad Api Authentication Attempt",
                    new { username, loginResult, ipAddress = HttpContext.Connection.RemoteIpAddress });
                return null;
            }

            var user = await _userService.Find(username);
            if (user == null) return null;

            var token = _jwtTokenService.GenerateJwtToken(user.Id);
            return token;
        }
    }
}
