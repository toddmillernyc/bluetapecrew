using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Models.Entities;
using Api.Services.Interfaces;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class ProductService : IProductService, IDisposable
    {
        private readonly BtcEntities _db = new BtcEntities();

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
            var product = await _db.Products.FirstOrDefaultAsync(x => x.LinkName.Equals(name));
            if (product == null) return null;

            var category = product.ProductCategories.Select(x=>x.Category).FirstOrDefault();
            var styles = product.Styles.OrderBy(x => x.Size.SizeOrder).ThenBy(x => x.Color.ColorText).ToList();
            var styleViews = _db.StyleViews.Where(x => x.ProductId == product.Id).OrderBy(x => x.SizeOrder).ThenBy(x => x.ColorText).ToList();

            var model = new ProductViewModel
            {
                Id = product.Id,
                AdditionalImages = product.ProductImages.Select(x=>x.Image).ToList().Select(
                    image => GetImgSrc(image.ImageData, image.MimeType))
                    .ToList(),
                AverageReview = product.Reviews.Any() ? product.Reviews.Average(x => x.Rating) : 0,
                BestSellers = _db.Products.Take(3).ToList().Select(prod => new BestSellerViewModel
                {
                    Id = prod.Id,
                    ImgSource = GetImgSrc(prod.Image.ImageData, prod.Image.MimeType),
                    LinkName = prod.LinkName,
                    Name = prod.LinkName,
                    Price = GetMoney(prod.Styles.FirstOrDefault().Price)
                }).ToList(),
                Description = product.Description,
                ImgSource = GetImgSrc(product.Image.ImageData, product.Image.MimeType),
                Menu = _db.Categories.Where(x => x.Published).OrderBy(x => x.CategoryName).Select(cat => new MenuViewModel
                {
                    Id = cat.Id,
                    MenuName = cat.CategoryName,
                    Items = cat.ProductCategories.Select(x=>x.Product).OrderBy(x => x.ProductName).Select(menuItem => new MenuItemViewModel
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

        public async Task<string> AddReview(int productId, string name, string email, string review, decimal rating)
        {
            _db.Reviews.Add(new Review
            {
                DateCreated = DateTime.UtcNow,
                Email = email,
                Name = name,
                ProductId = productId,
                Rating = rating,
                ReviewText = review
            });
            await _db.SaveChangesAsync();
            return _db.Products.Find(productId).LinkName;
        }

        public async Task<IEnumerable<Product>> GetBestSellers(int count = 3)
        {
            var randNum = new Random();
            var products = await _db.Products.Include(x => x.Image).Include(x => x.Styles).ToListAsync();
            var model = new List<Product>();
            for (var i = 0; i < count; i++)
            {
                var product = products[randNum.Next(products.Count)];
                products.Remove(product);
                model.Add(product);
            }
            return model;
        }

        public async Task<string> GetStylePrice(int id)
        {
            var style = await _db.Styles.FindAsync(id);
            return $"{style.Price:n2}";
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}