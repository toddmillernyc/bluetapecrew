using System.Threading.Tasks;
using System.Web.Mvc;
using BlueTapeCrew.Interfaces;

namespace BlueTapeCrew.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IProductService _productService;
        private readonly ICookieService _cookieService;

        public ProductsController(IImageService imageService,
                                  IProductService productService,
                                  ICookieService cookieService)
        {
            _imageService = imageService;
            _productService = productService;
            _cookieService = cookieService;
        }

        [Route("products/{name}")]
        [HttpGet]
        public async Task<ActionResult> Details(string name)
        {
            if (name.ToLower().Equals("details")) return RedirectToAction("Index", "Home");
            if (string.IsNullOrEmpty(name)) return RedirectToAction("Index", "Home");
            var productViewModel = await _productService.GetProductViewModel(name);
            if (productViewModel == null) return RedirectToAction("Index", "Home");
            _cookieService.SetCurrentProduct(System.Web.HttpContext.Current, productViewModel.Id);
            _cookieService.SetCurrentCategory(System.Web.HttpContext.Current, productViewModel.Category);
            return View(productViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("addreview")]
        public async Task<ActionResult> AddReview(int productId, string name, string email, string review, decimal rating)
        {
            return RedirectToAction("Details", new { name = await _productService.AddReview(productId, name, email, review, rating) });
        }

        [Route("product-pop-up/{name}")]
        public async Task<PartialViewResult> _ProductPopUp(string name)
        {
            return PartialView(await _productService.GetProductViewModel(name));
        }

        public async Task<PartialViewResult> _BestSellers()
        {
            return PartialView(await _productService.GetBestSellers(3));
        }

        public async Task<string> GetStylePrice(int id)
        {
            return await _productService.GetStylePrice(id);
        }
    }
}
