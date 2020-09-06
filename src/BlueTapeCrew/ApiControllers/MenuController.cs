using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueTapeCrew.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService) { _menuService = menuService; }
        public async Task<IEnumerable<MenuCategory>> Get() => await _menuService.Get();
    }
}