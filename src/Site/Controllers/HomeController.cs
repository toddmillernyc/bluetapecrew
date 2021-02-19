using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Services.Interfaces;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IViewModelService _viewModelService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IViewModelService viewModelService,
            ILogger<HomeController> logger)
        {
            _viewModelService = viewModelService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("**********Visit Homepage**********");
            var model = await _viewModelService.GetHomeViewModel();
            return View(model);
        }

        public async Task<PartialViewResult> _Sidebar()
        {
            return PartialView(await _viewModelService.GetSidebarViewModel());
        }
    }
}
