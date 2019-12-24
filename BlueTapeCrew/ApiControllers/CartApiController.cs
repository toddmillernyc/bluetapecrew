using BlueTapeCrew.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace BlueTapeCrew.ApiControllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartApiController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ISessionService _session;

        public CartApiController(ICartService cartService, ISessionService session)
        {
            _cartService = cartService;
            _session = session;
        }

        [HttpPost]
        [Route("{styleId}/{quantity}")]
        public async Task<IActionResult> Post(int styleId, int quantity)
        {
            await _cartService.AddOrUpdate(_session.SessionId(), styleId, quantity);
            return Ok();
        }
    }
}
