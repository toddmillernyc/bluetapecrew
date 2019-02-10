using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BlueTapeCrew.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartCalculatorService _cartCartCalculatorService;

        public CartController(ICartService cartService, ICartCalculatorService cartCartCalculatorService)
        {
            _cartService = cartService;
            _cartCartCalculatorService = cartCartCalculatorService;
        }

        [Route("cart")]
        public async Task<ActionResult> Details()
        {
            var cart = await _cartService.Get(Session.SessionID);
            var totals = await _cartCartCalculatorService.CalculateCartTotals(cart);
            return View(new CartViewModel(cart, totals));                
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