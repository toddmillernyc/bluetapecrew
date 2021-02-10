using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;

namespace Site.Api.Mobile
{
    [Route("api/mobile/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public CategoriesController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IEnumerable<MobileCategory>> Get() => await _menuService.GetMobileMenu();
    }
}
