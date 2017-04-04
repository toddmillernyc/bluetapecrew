using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BlueTapeCrew.Areas.Admin.Models;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCategoriesController : Controller
    {
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var db = new BtcEntities())
            {
                return View(db.Categories.Find(id));
            }
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            using (var db = new BtcEntities())
            {
                var existingCategory = db.Categories.Find(category.Id);
                existingCategory.CategoryName = category.CategoryName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Index()
        {
            using (var db = new BtcEntities())
            {
                var productList = new List<Product>();

                var categories = 
                db.Categories.OrderBy(category => category.CategoryName)
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
                ViewBag.ProductId = new SelectList(db.Products.OrderBy(x => x.ProductName).ToList(), "Id", "ProductName");
                return View(categories);
            }
        }
        public static List<AdminCategoryViewModel> GetCategoryList()
        {
            using (var db = new BtcEntities())
            {
                var categories = 
                    db.Categories.OrderBy(category => category.CategoryName)
                        .Select(category => new AdminCategoryViewModel
                        {
                            Id = category.Id,
                            Name = category.CategoryName,
                            ImageId = category.ImageId,
                            Products = category.Products.OrderBy(product => product.ProductName).Select(product => new AdminProductViewModel
                            {
                                Description = product.Description,
                                Id = product.Id,
                                ImageId = product.ImageId,
                                Name = product.ProductName
                            }).ToList()
                        }).ToList();
                return categories;
            }
        }
        public ActionResult Delete(int id)
        {
            using (var db = new BtcEntities())
            {
                var cat = db.Categories.Find(id);
                var products = cat.Products.ToList();
                foreach (var product in products)
                {
                    cat.Products.Remove(product);
                }
                db.Categories.Remove(cat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Index(string categoryName)
        {
            using (var db = new BtcEntities())
            {
                var model = new Category { CategoryName = categoryName };
                db.Categories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            using (var db = new BtcEntities())
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "AdminProducts");
        }

        [HttpPost]
        public ActionResult AddProductToCategory(int productId, int categoryId)
        {
            using (var db = new BtcEntities())
            {
                var product = db.Products.Find(productId);
                var category = db.Categories.Find(categoryId);
                if (!category.Products.Contains(product))
                {
                    category.Products.Add(product);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveProductFromCategory(int productId, int categoryId)
        {
            using (var db = new BtcEntities())
            {
                var product = db.Products.Find(productId);
                var category = db.Categories.Find(categoryId);
                category.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult PublishCategory(int id)
        {
            using (var db = new BtcEntities())
            {
                var category = db.Categories.Find(id);
                category.Published = !category.Published;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}