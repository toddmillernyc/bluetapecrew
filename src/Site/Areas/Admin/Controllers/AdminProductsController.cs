using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using Services.Interfaces;
using Services.Models;
using Site.Areas.Admin.Models;

namespace Site.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly IStyleService _styleService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public AdminProductsController(
            IProductService productService,
            IImageService imageService,
            ISiteSettingsService siteSettingsService,
            IStyleService styleService,
            ICategoryService categoryService,
            IMapper mapper)
        {
            _productService = productService;
            _imageService = imageService;
            _siteSettingsService = siteSettingsService;
            _styleService = styleService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddAdditionalImage(int productId, IFormFile file)
        {
            var saveImageRequest = _mapper.Map<SaveImageRequest>(file);
            var image = await _imageService.SaveImage(saveImageRequest);
            await _productService.AddImageToProduct(productId, image.Id);
            return Ok(image.Id);
        }

        [HttpPost]
        public async Task<string> DeleteStyle(int id)
        {
            await _styleService.DeleteStyle(id);
            return "1";
        }

        public async Task<JObject> SiteSettings() => JObject.FromObject(await _siteSettingsService.Get());

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/AdminProducts/CreateColor")]
        public async Task<IActionResult> CreateColor(int productId, string newColor)
        {
            var color = new Color {ColorText = newColor};
            await _styleService.CreateColor(color);
            return RedirectToAction("Edit", new { id = productId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/AdminProducts/CreateSize")]
        public async Task<IActionResult> CreateSize(int productId, string size)
        {
            await _styleService.CreateSize(size);
            return RedirectToAction("Edit", new { id = productId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/AdminProducts/CreateStyle")]
        public async Task<IActionResult> CreateStyle(Style style)
        {
            await _styleService.CreateStyle(style);
            return RedirectToAction("Edit", "AdminProducts", new { id = style.ProductId });
        }

        public async Task<IActionResult> Index()
        {
            var categories =
                (await _categoryService.GetAllWithProducts()).OrderBy(category => category.Name)
                    .Select(category => new AdminCategoryViewModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        ImageId = category.ImageId,
                        Products = category.ProductCategories.Select(x => x.Product).OrderBy(product => product.ProductName).Select(product => new AdminProductViewModel
                        {
                            Description = product.Description,
                            Id = product.Id,
                            ImageId = product.ImageId,
                            Name = product.ProductName
                        }).ToList()
                    });
            return View(categories);
        }

        [HttpGet]
        [Route("Admin/AdminProducts/Create")]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllWithProducts();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/AdminProducts/Create")]
        public async Task<IActionResult> Create(Product product, IFormFile file, int categoryId)
        {
            if (ModelState.IsValid)
            {
                var saveImageRequest = _mapper.Map<SaveImageRequest>(file);
                var image = await _imageService.SaveImage(saveImageRequest);
                product.ImageId = image.Id;
                await _productService.Create(product);
                await _categoryService.AddProductCategory(new ProductCategory { CategoryId  = categoryId, ProductId = product.Id});
                return RedirectToAction("Edit", "AdminProducts", new { id = product.Id });
            }

            var categories = await _categoryService.GetAllWithProducts();
            ViewBag.CategoryId = new SelectList(categories, "Id", "CategoryName", product.ProductCategories.FirstOrDefault()?.CategoryId);
            return View(product);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<int> SaveProductImage(int productId, IFormFile file)
        {
            var product = await _productService.Find(productId);
            var saveImageRequest = _mapper.Map<SaveImageRequest>(file);
            var image = await _imageService.SaveImage(saveImageRequest);
            if (product.Image != null) await _imageService.Delete(image.Id);
            await _productService.AddImageToProduct(productId, image.Id);
            return image.Id;
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return RedirectToAction("Index");
            var colors = await _styleService.GetColors();
            var sizes = await _styleService.GetSizes();

            ViewBag.ColorId = new SelectList(colors, "Id", "ColorText");
            ViewBag.SizeId = new SelectList(sizes, "Id", "SizeText");

            var products = await _productService.FindIncludeAll(id);
            var model = new EditProductViewModel
            {
                Product = await _productService.FindIncludeAll(id),
            Colors = colors?.OrderBy(x => x.ColorText),
                Sizes = sizes?.OrderBy(x => x.SizeOrder)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/AdminProducts/Edit")]
        public async Task<ActionResult> Edit(Product product, IFormFile file)
        {
            if (!ModelState.IsValid) return RedirectToAction("Edit", "AdminProducts", new {id = product.Id});
            
            await _productService.Update(product);
            if (file == null) return RedirectToAction("Edit", "AdminProducts", new {id = product.Id});

            if (product.ImageId > 0)
            {
                var oldImageId = product.ImageId;
                var saveImageRequest = _mapper.Map<SaveImageRequest>(product.Image);
                var image = await _imageService.SaveImage(saveImageRequest);
                product.ImageId = image.Id;
                await _imageService.Delete((int) oldImageId);
            }
            else
            {
                var saveImageRequest = _mapper.Map<SaveImageRequest>(file);
                var image = await _imageService.SaveImage(saveImageRequest);
                product.ImageId = image.Id;
            }
            await _productService.Update(product);
            return RedirectToAction("Edit", "AdminProducts", new {id = product.Id});
        }

        [HttpGet]
        [Route("Admin/AdminProducts/Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productService.FindIncludeAll(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Admin/AdminProducts/Delete/{id}")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _productService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
