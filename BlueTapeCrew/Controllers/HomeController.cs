using BlueTapeCrew.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlueTapeCrew.Controllers
{
    public class HomeController : Controller
    {
        private readonly IViewModelService _viewModelService;

        public HomeController(IViewModelService viewModelService,
                              IEmailSubscriptionService iEmailSubscriptionService)
        {
            _viewModelService = viewModelService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _viewModelService.GetHomeViewModel());
        }

        public async Task<PartialViewResult> _Sidebar()
        {
            return PartialView(await _viewModelService.GetSidebarViewModel());
        }
    }
}
