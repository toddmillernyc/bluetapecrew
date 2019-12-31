using Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BlueTapeCrew.Services;
using Services.Interfaces;

namespace BlueTapeCrew.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICookieService _cookieService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IViewModelService _viewModelService;

        public ProductsController(IProductService productService,
                                  ICookieService cookieService,
                                  IHttpContextAccessor httpContextAccessor,
                                  IViewModelService viewModelService)
        {
            _productService = productService;
            _cookieService = cookieService;
            _httpContextAccessor = httpContextAccessor;
            _viewModelService = viewModelService;
        }

        [Route("products/{name}")]
        [HttpGet]
        public async Task<ActionResult> Details(string name)
        {
            if (name.ToLower().Equals("details")) return RedirectToAction("Index", "Home");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Index", "Home");
            var productViewModel = await _viewModelService.GetProductViewModel(name);
            if (productViewModel == null) return RedirectToAction("Index", "Home");
            _cookieService.SetCurrentProduct(_httpContextAccessor.HttpContext, productViewModel.Id);
            _cookieService.SetCurrentCategory(_httpContextAccessor.HttpContext, productViewModel.Category);
            ViewBag.ReturnUrl = HttpContext.Request.Path.ToString();
            return View(productViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("addreview")]
        public async Task<ActionResult> AddReview(Review review) => RedirectToAction("Details", new { name = await _productService.AddReview(review) });

        public async Task<string> GetStylePrice(int id) => await _productService.GetStylePrice(id);
    }
}