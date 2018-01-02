using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BlueTapeCrew.Areas.Admin.Models;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;
using Newtonsoft.Json.Linq;

namespace BlueTapeCrew.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminProductsController : Controller
    {
        private readonly BtcEntities _db = new BtcEntities();

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<int> AddAdditionalImage(int productId,HttpPostedFileBase file)
        {
            using (var db = new BtcEntities())
            {
                var product = await db.Products.FindAsync(productId);
                try
                {
                    var image = await db.Images.FindAsync(SaveImage(file));
                    product.Images.Add(image);
                    await db.SaveChangesAsync();
                    return image.Id;
                }
                catch(Exception ex)
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
                }
                return 0;
            }
        }

        [HttpPost]
        public string DeleteStyle(int id)
        {
            using (var db = new BtcEntities())
            {
                var style = db.Styles.Find(id);
                db.Styles.Remove(style);
                foreach (var item in db.Carts.Where(x => x.StyleId.Equals(id)).ToList())
                {
                    db.Carts.Remove(item);
                }
                try
                {
                    db.SaveChanges();
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
        }

        public string DeleteImage(int imageId)
        {
            using (var db = new BtcEntities())
            {
                var image = db.Images.Find(imageId);
                try
                {
                    foreach (var item in db.Products.ToList())
                    {
                        var productImage = item.Images.FirstOrDefault(x => x.Id == imageId);
                        if (productImage != null)
                        {
                            item.Images.Remove(productImage);
                        }
                    }
                    db.Images.Remove(image);
                    db.SaveChanges();
                    return "1";
                }
                catch(Exception ex)
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
        }

        public JObject SiteSettings()
        {
            using (var db = new BtcEntities())
            {
                var model = db.SiteSettings.FirstOrDefault();
                return JObject.FromObject(model);
            }
        }

        [HttpPost]
        public ActionResult CreateColor(int productId, string newColor)
        {
            using (var db = new BtcEntities())
            {
                db.Colors.Add(new Color
                {
                    ColorText = newColor
                });
                db.SaveChanges();
            }
            return RedirectToAction("Edit", new { @id = productId });
        }

        [HttpPost]
        public ActionResult CreateSize(int productId, string size)
        {
            using (var db = new BtcEntities())
            {
                db.Sizes.Add(new Size
                {
                    SizeText = size,
                    SizeOrder = db.Sizes.OrderByDescending(x => x.SizeOrder).FirstOrDefault().SizeOrder + 1
                });
                db.SaveChanges();
            }
            return RedirectToAction("Edit", new { @id = productId });
        }

        [HttpPost]
        public ActionResult CreateStyle([Bind(Include = "ProductId,ColorId,SizeId,Price,InventoryCount")] Style style)
        {
            using (var db = new BtcEntities())
            {
                db.Styles.Add(style);
                db.SaveChanges();
            }
            return RedirectToAction("Edit", "AdminProducts", new { @id = style.ProductId });
        }

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return Redirect("~/Account/Login");
            }
            return View(GetCategoryList());
        }


        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "CategoryName", null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ProductName,Description,LinkName")] Product product, HttpPostedFileBase file, int categoryId)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    product.ImageId = SaveImage(file);
                }
                var category = _db.Categories.Find(categoryId);
                product.Categories.Add(category);
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("Edit", "AdminProducts", new { @id = product.Id });
            }

            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "CategoryName", product.Categories.FirstOrDefault()?.Id);
            return View(product);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<int> SaveProductImage(int productId,HttpPostedFileBase file)
        {
            using (var db = new BtcEntities())
            {
                var product = await db.Products.FindAsync(productId);
                var imageId = SaveImage(file);
                if (product.Image != null)
                {
                    db.Images.Remove(product.Image);
                }
                product.ImageId = imageId;
                await db.SaveChangesAsync();
                return imageId;
            }
        }

        public static int SaveImage(HttpPostedFileBase file)
        {
            using (var db = new BtcEntities())
            using (var target = new MemoryStream())
            {
                file.InputStream.CopyTo(target);
                var data = target.ToArray();
                var fileName = file.FileName;
                int c = 0;
                while (true)
                {
                    if (!db.Images.Any(x => x.Name.Equals(fileName))) break;
                    c++;
                    var tokens = file.FileName.Split('.');
                    fileName = tokens[0] + "(" + c + ")." + tokens[tokens.Length - 1];
                }

                var image = new Image
                {
                    Name = fileName,
                    ImageData = data,
                    MimeType = MimeMapping.GetMimeMapping(file.ContentType)
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

                db.Images.Add(image);
                db.SaveChanges();
                return image.Id;
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.ColorId = new SelectList(_db.Colors, "Id", "ColorText");
            ViewBag.SizeId = new SelectList(_db.Sizes, "Id", "SizeText");
            using (var db = new BtcEntities())
            {
                var product = db.Products.Include(x=>x.Image).Include(x=>x.Styles).FirstOrDefault(x => x.Id == id);
                var model = new EditProductViewModel
                {
                    Product = product,
                    Colors = db.Colors.OrderBy(x => x.ColorText).ToList(),
                    Sizes = db.Sizes.OrderBy(x => x.SizeOrder).ToList()
                };

                if (product!=null && product.Images.Any())
                {
                    model.Images = product.Images.ToList();
                }
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ImageId,ProductName,Description,LinkName")] Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(product).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                if (file != null)
                {
                    if (product.ImageId > 0)
                    {
                        var oldImageId = product.ImageId;
                        product.ImageId = SaveImage(file);
                        _db.SaveChanges();
                        DeleteImage(oldImageId);
                    }
                    else
                    {
                        product.ImageId = SaveImage(file);
                    }
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Edit", "AdminProducts", new {id = product.Id});
        }

        public static void DeleteImage(int? id)
        {
            using (var db = new BtcEntities())
            {
                var image = db.Images.Find(id);
                db.Images.Remove(image);
                db.SaveChanges();
            }
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await _db.Products.FindAsync(id);
            var image = product.Image;
            product.Image = null;
            if (product.Categories.Any())
            {
                foreach (var cat in product.Categories.ToList())
                {
                    cat.Products.Remove(product);
                }
            }
            foreach (var item in product.Styles.ToList())
            {
                _db.Styles.Remove(item);
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            if (image != null)
            {
                _db.Images.Remove(image);
                await _db.SaveChangesAsync();
            }
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
