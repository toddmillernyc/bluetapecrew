using BlueTapeCrew.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlueTapeCrew.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IShippingService _shippingService;

        public CartController(ICartService cartService, IShippingService shippingService)
        {
            _cartService = cartService;
            _shippingService = shippingService;
        }

        [Route("cart")]
        public async Task<ActionResult> Details()
        {
            var cart = await _cartService.Get(Session.SessionID);
            var subTotal = cart.Sum(x => x.SubTotal);
            var shipping = await _shippingService.Caclulate(subTotal ?? 0.00m);
            ViewBag.Shipping = $"{shipping:n2}";
            ViewBag.SubTotal = $"{subTotal:n2}";
            ViewBag.Total = $"{(subTotal + shipping) + 6:n2}";
            return View(cart);                
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