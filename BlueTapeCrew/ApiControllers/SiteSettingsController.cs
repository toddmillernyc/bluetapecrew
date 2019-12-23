using BlueTapeCrew.Services.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlueTapeCrew.ApiControllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class SiteSettingsController : ControllerBase
    {
        private readonly ISiteSettingsService _siteSettingsService;

        public SiteSettingsController(ISiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        [HttpGet]
        public async Task<SiteSetting> Get()
        {
            return await _siteSettingsService.Get();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SiteSetting siteSetting)
        {
            if (siteSetting.Id > 0) await _siteSettingsService.Set(siteSetting);
            else return BadRequest("You tried to save settings without an Id");
            return Ok();
        }
    }
}
