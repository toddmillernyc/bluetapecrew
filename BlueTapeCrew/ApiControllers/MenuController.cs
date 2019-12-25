using System.Collections.Generic;
using System.Threading.Tasks;
using BlueTapeCrew.Models;
using BlueTapeCrew.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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