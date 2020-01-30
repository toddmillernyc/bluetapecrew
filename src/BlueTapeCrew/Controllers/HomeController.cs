using BlueTapeCrew.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlueTapeCrew.Controllers
{
    public class HomeController : Controller
    {
        private readonly IViewModelService _viewModelService;

        public HomeController(IViewModelService viewModelService)
        {
            _viewModelService = viewModelService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _viewModelService.GetHomeViewModel();
            return View(model);
        }

        public async Task<PartialViewResult> _Sidebar()
        {
            return PartialView(await _viewModelService.GetSidebarViewModel());
        }
    }
}
