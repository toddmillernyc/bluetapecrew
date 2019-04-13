using BlueTapeCrew.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using BlueTapeCrew.Email;

namespace BlueTapeCrew.Areas.Api.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class EmailTestController : ApiController
    {
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly IEmailService _emailService;

        public EmailTestController(ISiteSettingsService siteSettingsService, IEmailService emailService)
        {
            _siteSettingsService = siteSettingsService;
            _emailService = emailService;
        }

        public async Task<IHttpActionResult> Get(string email)
        {
            try
            {
                var request = EmailHelper.GetTestEmailRequest(await _siteSettingsService.Get(), email);
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
