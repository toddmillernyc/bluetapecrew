using System.Linq;
using System.Web.Mvc;
using BlueTapeCrew.Areas.Admin.Models;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCategoriesController : Controller
    {
        readonly BtcEntities _db = new BtcEntities();

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(_db.Categories.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            var existingCategory = _db.Categories.Find(category.Id);
            if (existingCategory != null) existingCategory.CategoryName = category.CategoryName;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            var categories =
            _db.Categories.OrderBy(category => category.CategoryName)
            .Select(category => new AdminCategoryViewModel
            {
                Id = category.Id,
                Name = category.CategoryName,
                ImageId = category.ImageId,
                Published = category.Published,
                Products = category.Products.OrderBy(product => product.ProductName).Select(product => new AdminProductViewModel
                {
                    Description = product.Description,
                    Id = product.Id,
                    ImageId = product.ImageId,
                    Name = product.ProductName
                }).ToList()
            }).ToList();
            ViewBag.ProductId = new SelectList(_db.Products.OrderBy(x => x.ProductName).ToList(), "Id", "ProductName");
            return View(categories);
        }

        public ActionResult Delete(int id)
        {
            var cat = _db.Categories.Find(id);
            if (cat == null) return HttpNotFound();

            var products = cat.Products.ToList();
            foreach (var product in products) cat.Products.Remove(product);
            _db.Categories.Remove(cat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(string categoryName)
        {
            var model = new Category { CategoryName = categoryName };
            _db.Categories.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index", "AdminProducts");
        }

        [HttpPost]
        public ActionResult AddProductToCategory(int productId, int categoryId)
        {
            var product = _db.Products.Find(productId);
            var category = _db.Categories.Find(categoryId);
            if (category == null || category.Products.Contains(product)) return RedirectToAction("Index");
            category.Products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveProductFromCategory(int productId, int categoryId)
        {
            var product = _db.Products.Find(productId);
            var category = _db.Categories.Find(categoryId);
            category?.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PublishCategory(int id)
        {
            var category = _db.Categories.Find(id);
            if (category != null) category.Published = !category.Published;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public new void Dispose()
        {
            _db?.Dispose();
        }
    }
}