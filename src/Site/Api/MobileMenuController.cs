using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces; 
using Services.Models;

namespace Site.Api
{
    [Route("api/mobile/menu")]
    [ApiController]
    public class MobileMenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MobileMenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IEnumerable<MobileCategory>> Get() => await _menuService.GetMobileMenu();
    }
}
