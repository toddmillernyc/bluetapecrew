using System.Web.Http;
using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Services;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SiteSettingsController : ApiController
    {
        private readonly ISiteSettingsService _siteSettingsService;

        public SiteSettingsController(ISiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        public async Task<SiteSetting> Get()
        {
            return await _siteSettingsService.Get();
        }

        public async Task<IHttpActionResult> Post([FromBody]SiteSetting siteSetting)
        {
            if (siteSetting.Id > 0) await _siteSettingsService.Set(siteSetting);
            else return BadRequest("You tried to save settings without an Id");
            return Ok();
        }
    }
}
