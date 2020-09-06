using AutoMapper;
using BlueTapeCrew.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;
using Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BlueTapeCrew.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public AdminCategoriesController(
            ICategoryService categoryService,
            IProductService productService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) => View(await _categoryService.Find(id));


        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            await _categoryService.ChangeName(category.Id, category.Name);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllWithProducts();
            var viewModel = categories.Select(category => _mapper.Map<AdminCategoryViewModel>(category));
            ViewBag.ProductId = new SelectList(await _productService.GetProductNames(), "Key", "Value");
            return View(new AdminCategoryPageViewModel(viewModel));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Index(string categoryName)
        {
            await _categoryService.Create(new Category {Name = categoryName});
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _categoryService.Create(category);
            return RedirectToAction("Index", "AdminProducts");
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCategory(int productId, int categoryId)
        {
            await _categoryService.AddProductCategory(new ProductCategory {ProductId = productId, CategoryId = categoryId});
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveProductFromCategory(int productId, int categoryId)
        {
            await _categoryService.DeleteProductCategory(new ProductCategory {CategoryId = categoryId, ProductId = productId});
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> PublishCategory(int id)
        {
            await _categoryService.TogglePublish(id);
            return RedirectToAction("Index");
        }
    }
}