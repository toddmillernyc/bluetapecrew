using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
    using Site.Services.Interfaces;

namespace Site.Controllers
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

        [Route("Home/Error/{statusCode:int}")]
        public IActionResult Error(int statusCode)
        {
            var errorMessage = "Error page redirect";
            switch (statusCode)
            {
                case 404:
                    errorMessage = $"{statusCode} Page not found.";
                    break;
            }
            ViewBag.errorMessage = errorMessage;
            Response.StatusCode = statusCode;
            return View("Error");
        }

        public async Task<PartialViewResult> _Sidebar()
        {
            return PartialView(await _viewModelService.GetSidebarViewModel());
        }
    }
}
