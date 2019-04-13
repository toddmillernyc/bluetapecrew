using System;
using Api.Models.Entities;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class SiteSettingsController : ControllerBase
    {
        private readonly ISiteSettingsService _siteSettingsService;

        public SiteSettingsController(ISiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        public async Task<IActionResult> Get()
        {
            try
            {
                var settings = await _siteSettingsService.Get();
                return Ok(settings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
