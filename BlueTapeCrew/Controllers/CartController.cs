using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlueTapeCrew.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) { _cartService = cartService; }

        private Task<CartViewModel> Cart => _cartService.GetCartViewModel(Session.SessionID);

        [Route("cart")]
        public async Task<ActionResult> Details() => View(await Cart);

        public async Task<PartialViewResult> Index() => PartialView(await Cart);

        [HttpPost]
        public async Task Post(int styleId,int quantity) => await _cartService.AddOrUpdate(Session.SessionID, styleId, quantity);

        public async Task Delete(int id) => await _cartService.DecrementCartItem(id);
    }
}