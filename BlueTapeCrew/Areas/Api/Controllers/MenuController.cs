using BlueTapeCrew.Models;
using BlueTapeCrew.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlueTapeCrew.Areas.Api.Controllers
{
    public class MenuController : ApiController
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IEnumerable<MenuCategory>> Get()
        {
            return await _menuService.Get();
        }
    }
}