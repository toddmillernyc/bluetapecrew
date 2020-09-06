using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using Site.Services;

namespace Site.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ISessionService _session;

        public CartController(
            ICartService cartService,
            ISessionService session)
        {
            _cartService = cartService;
            _session = session;
        }

        private Task<CartViewModel> Cart => _cartService.GetCartViewModel(_session.SessionId());
        [Route("cart")]
        public async Task<ActionResult> Details() => View(await Cart);
        public async Task Delete(int id) => await _cartService.DecrementCartItem(id);
    }
}