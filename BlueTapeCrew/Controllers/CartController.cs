using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlueTapeCrew.Contracts.Services;

namespace BlueTapeCrew.Controllers
{   
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ISiteSettingsService _siteSettingsService;

        public CartController(ICartService cartService, ISiteSettingsService siteSettingsService)
        {
            _cartService = cartService;
            _siteSettingsService = siteSettingsService;
        }

        [Route("cart")]
        public async Task<ActionResult> Details()
        {
            var cart = await _cartService.Get(Session.SessionID);
            var settings = await _siteSettingsService.GetSettings();

            var subTotal = cart.Sum(x => x.SubTotal);
            ViewBag.SubTotal = $"{subTotal:n2}";
            if (subTotal > settings.FreeShippingThreshold)
            {
                ViewBag.Shipping = "0.00";
                ViewBag.Total = ViewBag.SubTotal;
            }
            else
            {
                ViewBag.Shipping = $"{settings.FlatShippingRate:n2}";
                ViewBag.Total = $"{subTotal + 6:n2}";
            }
            return View(cart);                
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetQuantity(int styleId, int quantity)
        {
            await _cartService.SetQuantity(Session.SessionID,styleId,quantity);
            return RedirectToAction("cart");
        }

        public async Task<PartialViewResult> Index()
        {
            var model = await _cartService.GetCartViewModel(Session.SessionID);
            return PartialView(model);
        }
        
        [HttpPost]
        public async Task<int> Post(int styleId,int quantity)
        {
            return await _cartService.Post(Session.SessionID, styleId, quantity);
        }

        public async Task Delete(int id)
        {
            await _cartService.DeleteItem(id);
        }
    }
}