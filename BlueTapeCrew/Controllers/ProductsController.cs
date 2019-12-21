using BlueTapeCrew.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlueTapeCrew.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICookieService _cookieService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductsController(IProductService productService,
                                  ICookieService cookieService,
                                  IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _cookieService = cookieService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Route("products/{name}")]
        [HttpGet]
        public async Task<ActionResult> Details(string name)
        {
            if (name.ToLower().Equals("details")) return RedirectToAction("Index", "Home");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Index", "Home");
            var productViewModel = await _productService.GetProductViewModel(name);
            if (productViewModel == null) return RedirectToAction("Index", "Home");
            _cookieService.SetCurrentProduct(_httpContextAccessor.HttpContext, productViewModel.Id);
            _cookieService.SetCurrentCategory(_httpContextAccessor.HttpContext, productViewModel.Category);
            ViewBag.ReturnUrl = HttpContext.Request.Path.ToString();
            return View(productViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("addreview")]
        public async Task<ActionResult> AddReview(int productId, string name, string email, string review, decimal rating)
        {
            return RedirectToAction("Details", new { name = await _productService.AddReview(productId, name, email, review, rating) });
        }

        public async Task<PartialViewResult> _BestSellers()
        {
            return PartialView(await _productService.GetBestSellers());
        }

        public async Task<string> GetStylePrice(int id)
        {
            return await _productService.GetStylePrice(id);
        }
    }
}