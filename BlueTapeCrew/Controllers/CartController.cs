using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BlueTapeCrew.Services;
using Services.Interfaces;
using Services.Models;

namespace BlueTapeCrew.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ISessionService _session;

        public CartController(ICartService cartService, ISessionService session)
        {
            _cartService = cartService;
            _session = session;
        }

        private Task<CartViewModel> Cart => _cartService.GetCartViewModel(_session.SessionId());

        [Route("cart")]
        public async Task<ActionResult> Details() => View(await Cart);

        public async Task<PartialViewResult> Index() => PartialView(await Cart);

        public async Task Delete(int id) => await _cartService.DecrementCartItem(id);
    }
}