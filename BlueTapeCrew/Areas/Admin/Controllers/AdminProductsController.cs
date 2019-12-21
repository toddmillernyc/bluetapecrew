using BlueTapeCrew.Areas.Admin.Models;
using BlueTapeCrew.Data;
using BlueTapeCrew.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlueTapeCrew.Services.Interfaces;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace BlueTapeCrew.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly IProductService _productService;

        private readonly BtcEntities _db;

        public AdminProductsController(BtcEntities db, IProductService productService)
        {
            _db = db;
            _productService = productService;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddAdditionalImage(int productId, IFormFile file)
        {
            var product = await _db.Products.FindAsync(productId);
            try
            {
                var imageId = await SaveImage(file);
                var image = await _db.Images.FindAsync(imageId);
                product.ProductImages.Add(new ProductImage
                {
                    ProductId = productId,
                    ImageId = imageId
                });
                await _db.SaveChangesAsync();
                return Ok(image.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public string DeleteStyle(int id)
        {
            var style = _db.Styles.Find(id);
            _db.Styles.Remove(style);
            foreach (var item in _db.Carts.Where(x => x.StyleId.Equals(id)).ToList())
            {
                _db.Carts.Remove(item);
            }
            try
            {
                _db.SaveChanges();
                return "1";
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (ex.InnerException != null)
                {
                    msg += ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null)
                    {
                        msg += ex.InnerException.InnerException.Message;
                    }
                }
                return msg;
            }
        }

        public JObject SiteSettings()
        {
            var model = _db.SiteSettings.FirstOrDefault();
            return JObject.FromObject(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/AdminProducts/CreateColor")]
        public ActionResult CreateColor(int productId, string newColor)
        {
            _db.Colors.Add(new Color { ColorText = newColor });
            _db.SaveChanges();
            return RedirectToAction("Edit", new { id = productId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/AdminProducts/CreateSize")]
        public async Task<IActionResult> CreateSize(int productId, string size)
        {
            var lastSize = await _db.Sizes.OrderByDescending(x => x.SizeOrder).FirstOrDefaultAsync();
            var sizeOrder = lastSize?.SizeOrder ?? 0;
            _db.Sizes.Add(new Size
            {
                SizeText = size,
                SizeOrder = sizeOrder + 1
            });
            _db.SaveChanges();
            return RedirectToAction("Edit", new { id = productId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/AdminProducts/CreateStyle")]
        public ActionResult CreateStyle(Style style)
        {
            _db.Styles.Add(style);
            _db.SaveChanges();
            return RedirectToAction("Edit", "AdminProducts", new { id = style.ProductId });
        }

        public ActionResult Index()
        {
            return View(GetCategoryList());
        }

        [HttpGet]
        [Route("Admin/AdminProducts/Create")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "CategoryName", null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/AdminProducts/Create")]
        public async Task<ActionResult> Create(Product product, IFormFile file, int categoryId)
        {
            if (ModelState.IsValid)
            {
                if (file != null) product.ImageId = await SaveImage(file);
                var category = _db.Categories.Find(categoryId);
                product.ProductCategories.Add(new ProductCategory { Category = category, Product = product});
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("Edit", "AdminProducts", new { id = product.Id });
            }

            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "CategoryName", product.ProductCategories.FirstOrDefault()?.CategoryId);
            return View(product);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<int> SaveProductImage(int productId, IFormFile file)
        {
            var product = await _db.Products.FindAsync(productId);
            var imageId = await SaveImage(file);
            if (product.Image != null)
            {
                _db.Images.Remove(product.Image);
            }
            product.ImageId = imageId;
            await _db.SaveChangesAsync();
            return imageId;
        }

        public async Task<int> SaveImage(IFormFile file)
        {
            await using var target = new MemoryStream();
            await file.CopyToAsync(target);

            var data = target.ToArray();
            var fileName = file.FileName;
            var c = 0;
            while (true)
            {
                if (!_db.Images.Any(x => x.Name.Equals(fileName))) break;
                c++;
                var tokens = file.FileName.Split('.');
                fileName = tokens[0] + "(" + c + ")." + tokens[^1];
            }

            var image = new Image
            {
                Name = fileName,
                ImageData = data,
                MimeType = file.ContentType
            };

            var ext = file.FileName.Split('.')[1];
            if (ext.Equals("jpg") || ext.Equals("jpeg"))
            {
                image.MimeType = "image/jpeg";
            }
            else if (ext.Equals("png"))
            {
                image.MimeType = "image/png";
            }

            _db.Images.Add(image);
            _db.SaveChanges();
            return image.Id;
        }
        
        public ActionResult Edit(int id)
        {
            if (id == 0) return RedirectToAction("Index");
            ViewBag.ColorId = new SelectList(_db.Colors, "Id", "ColorText");
            ViewBag.SizeId = new SelectList(_db.Sizes, "Id", "SizeText");
            var products = 
                _db.Products
                    .Include(x=>x.Image)
                    .Include(x=>x.ProductImages)
                    .ThenInclude(pi=>pi.Image)
                    .Include(x=>x.Styles)
                    .Where(x=>x.Id == id);
            var model = new EditProductViewModel
            {
                Product = products.FirstOrDefault(),
                Colors = _db.Colors.OrderBy(x => x.ColorText).ToList(),
                Sizes = _db.Sizes.OrderBy(x => x.SizeOrder).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/AdminProducts/Edit")]
        public async Task<ActionResult> Edit(Product product, IFormFile file)
        {
            if (!ModelState.IsValid) return RedirectToAction("Edit", "AdminProducts", new {id = product.Id});
            _db.Entry(product).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            if (file == null) return RedirectToAction("Edit", "AdminProducts", new {id = product.Id});
            if (product.ImageId > 0)
            {
                var oldImageId = product.ImageId;
                product.ImageId = await SaveImage(file);
                _db.SaveChanges();
                await DeleteImage((int)oldImageId);
            }
            else
            {
                product.ImageId = await SaveImage(file);
            }
            _db.SaveChanges();
            return RedirectToAction("Edit", "AdminProducts", new {id = product.Id});
        }

        public async Task DeleteImage(int? imageId)
        {
            var image = _db.Images.Find(imageId);
            var productImages = _db.ProductImages.Where(x => x.ImageId == imageId);
            _db.ProductImages.RemoveRange(productImages);
            _db.Images.Remove(image);
            await _db.SaveChangesAsync();
        }

        [HttpGet]
        [Route("Admin/AdminProducts/Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _db
                .Products
                .Include(x=>x.Image)
                .Include(x=>x.ProductImages)
                .ThenInclude(p=>p.Image)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
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

        public List<AdminCategoryViewModel> GetCategoryList()
        {
            var categories =
                _db.Categories.OrderBy(category => category.CategoryName)
                    .Select(category => new AdminCategoryViewModel
                    {
                        Id = category.Id,
                        Name = category.CategoryName,
                        ImageId = category.ImageId,
                        Products = category.ProductCategories.Select(x=>x.Product).OrderBy(product => product.ProductName).Select(product => new AdminProductViewModel
                        {
                            Description = product.Description,
                            Id = product.Id,
                            ImageId = product.ImageId,
                            Name = product.ProductName
                        }).ToList()
                    }).ToList();
            return categories;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db?.Dispose();
            base.Dispose(disposing);
        }
    }
}
