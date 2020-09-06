using System;
using System.Threading.Tasks;
using BlueTapeCrew.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Helpers;
using Services.Interfaces;
using Services.Models;

namespace BlueTapeCrew.ApiControllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class EmailTestController : ControllerBase
    {
        private const string TestEmailSubject = "Testing BlueTapeCrew Email System";

        private readonly ISiteSettingsService _siteSettingsService;
        private readonly IEmailService _emailService;
        private readonly ISessionService _sessionService;

        public EmailTestController(ISiteSettingsService siteSettingsService, IEmailService emailService, ISessionService sessionService)
        {
            _siteSettingsService = siteSettingsService;
            _emailService = emailService;
            _sessionService = sessionService;
        }

        public async Task<IActionResult> Get(string email)
        {
            try
            {
                var request = EmailHelper.GetTestEmailRequest(await _siteSettingsService.Get(), email, _sessionService.SessionId(), TestEmailSubject);
                await _emailService.SendEmail(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
