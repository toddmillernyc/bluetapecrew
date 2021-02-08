using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Site.Areas.Admin.Models;

namespace Site.Controllers.Admin
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
        public async Task<SiteSettingsViewModel> Get() => new SiteSettingsViewModel
        {
            SiteSettings = await _siteSettingsService.Get(),
            SiteProfile = await _siteSettingsService.GetSiteProfile()
        };

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SiteSettingsViewModel model)
        {
            try
            {
                await _siteSettingsService.Set(model.SiteSettings);
                await _siteSettingsService.SetSiteProfile(model.SiteProfile);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
