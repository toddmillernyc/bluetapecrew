using System.Threading.Tasks;
using System.Web.Mvc;
using BlueTapeCrew.Interfaces;

namespace BlueTapeCrew.Controllers
{
    public class HomeController : Controller
    {
        private readonly IViewModelService _viewModelService;
        private readonly IEmailSubscriptionService _emailSubscriptionService;

        public HomeController(IViewModelService viewModelService,
                              IEmailSubscriptionService iEmailSubscriptionService)
        {
            _viewModelService = viewModelService;
            _emailSubscriptionService = iEmailSubscriptionService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _viewModelService.GetHomeViewModel());
        }

        public async Task<PartialViewResult> _Sidebar()
        {
            return PartialView(await _viewModelService.GetSidebarViewModel());
        }

        [System.Web.Mvc.HttpPost]
        public string Subscribe(string subscribeEmail)
        {
            return _emailSubscriptionService.Subscribe(subscribeEmail);
        }

    }
}