using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlueTapeCrew.Contracts.Services;
using BlueTapeCrew.Models;
using BlueTapeCrew.ViewModels;

namespace BlueTapeCrew.Services
{
    public class ProductService : IProductService
    {
        private static string GetImgSrc(byte[] imageData, string mimeType)
        {
            return "data:" + mimeType + ";base64," + Convert.ToBase64String(imageData);
        }

        private static string GetMoney(decimal? m)
        {
            return m == null ? "0.00" : $"{m:n2}";
        }

        public async Task<ProductViewModel> GetProductViewModel(string name)
        {
            using (var db = new BtcEntities())
            {
                var product = await db.Products.FirstOrDefaultAsync(x => x.LinkName.Equals(name));
                if (product == null) return null;

                var category = product.Categories.FirstOrDefault();
                var styles = product.Styles.OrderBy(x => x.Size.SizeOrder).ThenBy(x => x.Color.ColorText).ToList();
                var styleViews = db.StyleViews.Where(x => x.ProductId == product.Id).OrderBy(x => x.SizeOrder).ThenBy(x => x.ColorText).ToList();

                var model = new ProductViewModel
                {
                    Id = product.Id,
                    AdditionalImages = product.Images.ToList().Select(
                        image => GetImgSrc(image.ImageData, image.MimeType))
                        .ToList(),
                    AverageReview = product.Reviews.Any() ? product.Reviews.Average(x => x.Rating) : 0,
                    BestSellers = db.Products.Take(3).ToList().Select(prod => new BestSellerViewModel
                    {
                        Id = prod.Id,
                        ImgSource = GetImgSrc(prod.Image.ImageData, prod.Image.MimeType),
                        LinkName = prod.LinkName,
                        Name = prod.LinkName,
                        Price = GetMoney(prod.Styles.FirstOrDefault().Price)
                    }).ToList(),
                    Description = product.Description,
                    ImgSource = GetImgSrc(product.Image.ImageData, product.Image.MimeType),
                    Menu = db.Categories.Where(x=>x.Published).OrderBy(x => x.CategoryName).Select(cat => new MenuViewModel
                    {
                        Id = cat.Id,
                        MenuName = cat.CategoryName,
                        Items = cat.Products.OrderBy(x => x.ProductName).Select(menuItem => new MenuItemViewModel
                        {
                            Id = menuItem.Id,
                            ItemName = menuItem.ProductName,
                            LinkName = menuItem.LinkName,
                        }).ToList()
                    }).ToList(),
                    Name = product.ProductName,
                    Price = GetMoney(styles.FirstOrDefault().Price),
                    Reviews = product.Reviews.OrderByDescending(x => x.DateCreated).Select(review => new ReviewViewModel
                    {
                        Content = review.ReviewText,
                        Date = review.DateCreated.ToString(),
                        Id = review.Id,
                        Name = review.Name,
                        Rating = review.Rating
                    }).ToList(),
                    StyleId = new SelectList(styleViews, "Id", "StyleText", styleViews.FirstOrDefault())
                };


                if (category != null)
                {
                    model.Category = category.CategoryName;
                }

                return model;
            }
        }

        public async Task<string> AddReview(int productId, string name, string email, string review, decimal rating)
        {
            using (var db = new BtcEntities())
            {
                db.Reviews.Add(new Review
                {
                    DateCreated = DateTime.UtcNow,
                    Email = email,
                    Name = name,
                    ProductId = productId,
                    Rating = rating,
                    ReviewText = review
                });
                await db.SaveChangesAsync();
                return db.Products.Find(productId).LinkName;
            }
        }

        public async Task<IEnumerable<Product>> GetBestSellers(int count=3)
        {
            using (var db = new BtcEntities())
            {
                var randNum = new Random();
                var products = await db.Products.Include(x => x.Image).Include(x => x.Styles).ToListAsync();
                var model = new List<Product>();
                for (var i = 0; i < count; i++)
                {
                    var product = products[randNum.Next(products.Count)];
                    products.Remove(product);
                    model.Add(product);
                }
                return model;
            }
        }

        public async Task<string> GetStylePrice(int id)
        {
            using (var db = new BtcEntities())
            {
                var style = await db.Styles.FindAsync(id);
                return $"{style.Price:n2}";
            }
        }
    }
}