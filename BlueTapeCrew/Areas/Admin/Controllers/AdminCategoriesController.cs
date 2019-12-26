using BlueTapeCrew.Areas.Admin.Models;
using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IProductCategoriesRepository _productCategoriesRepository;

        public AdminCategoriesController(
            ICategoryService categoryService,
            IProductService productService,
            IProductCategoriesRepository productCategoriesRepository)
        {
            _categoryService = categoryService;
            _productService = productService;
            _productCategoriesRepository = productCategoriesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) => View(await _categoryService.Find(id));


        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            await _categoryService.ChangeName(category.Id, category.CategoryName);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var categories = (await _categoryService.GetAll())
            .Select(category => new AdminCategoryViewModel
            {
                Id = category.Id,
                Name = category.CategoryName,
                ImageId = category.ImageId,
                Published = category.Published,
                Products = category.ProductCategories.Select(x=>x.Product).OrderBy(product => product.ProductName).Select(product => new AdminProductViewModel
                {
                    Description = product.Description,
                    Id = product.Id,
                    ImageId = product.ImageId,
                    Name = product.ProductName
                }).ToList()
            }).ToList();

            ViewBag.ProductId = new SelectList(await _productService.GetProductNames(), "Key", "Value");
            return View(categories);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Index(string categoryName)
        {
            await _categoryService.Create(new Category {CategoryName = categoryName});
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
            await _productCategoriesRepository.Create(new ProductCategory {ProductId = productId, CategoryId = categoryId});
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveProductFromCategory(int productId, int categoryId)
        {
            await _productCategoriesRepository.Delete(new ProductCategory {CategoryId = categoryId, ProductId = productId});
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