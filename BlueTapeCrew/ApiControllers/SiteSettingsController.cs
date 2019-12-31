using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Services.Interfaces;
using Services.Models;

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
        public async Task<SiteSetting> Get() => await _siteSettingsService.Get();

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SiteSetting siteSetting)
        {
            try
            {
                return Ok(await _siteSettingsService.Set(siteSetting));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
