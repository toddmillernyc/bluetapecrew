using BlueTapeCrew.Data;
using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Services
{
    public class ProductService : IProductService, IDisposable
    {
        private readonly BtcEntities _db;
        private readonly IProductRepository _productRepository;

        public ProductService(
            BtcEntities db,
            IProductRepository productRepository)
        {
            _db = db;
            _productRepository = productRepository;
        }

        //todo move to extension method
        private static string GetImgSrc(byte[] imageData, string mimeType) => "data:" + mimeType + ";base64," + Convert.ToBase64String(imageData);

        //todo: replace with string currency format
        private static string GetMoney(decimal? m) => m == null ? "0.00" : $"{m:n2}";

        public async Task<ProductViewModel> GetProductViewModel(string name)
        {
            var product = await _productRepository.FindBySlug(name);
            var bestSellers = await GetBestSellers();
            if (product == null) return null;
            var category = product.ProductCategories.FirstOrDefault()?.Category;
            var styles = product.Styles.OrderBy(x => x.Size.SizeOrder).ThenBy(x => x.Color.ColorText).ToList();
            var styleViews = _db.StyleViews.Where(x => x.ProductId == product.Id).OrderBy(x => x.SizeOrder).ThenBy(x => x.ColorText).ToList();

            var model = new ProductViewModel
            {
                Id = product.Id,
                AdditionalImages = product.ProductImages.Select(x=>x.Image).ToList().Select(
                    image => GetImgSrc(image.ImageData, image.MimeType))
                    .ToList(),
                AverageReview = product.Reviews.Any() ? product.Reviews.Average(x => x.Rating) : 0,
                BestSellers = bestSellers,
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

        public async Task Delete(int id)
        {
            var product = 
                await _db.Products
                         .Include(x => x.ProductImages)
                         .ThenInclude(x => x.Image)
                         .Include(x=>x.ProductCategories)
                         .Include(x=>x.CartImages)
                         .Where(x => x.Id == id)
                         .FirstOrDefaultAsync();

            var styles = await _db.Styles.Include(x=>x.Carts).Where(style => style.ProductId == id).ToListAsync();
            
            //delete carts
            if (styles.Any() && styles.Any(x => x.Carts.Any()))
            {
                foreach (var style in styles)
                    _db.Carts.RemoveRange(style.Carts);
                await _db.SaveChangesAsync();
            }

            if (styles.Any())
            {
                _db.Styles.RemoveRange(styles);
                await _db.SaveChangesAsync();
            }

            if (product.ImageId > 0)
            {
                var image = await _db.Images.FindAsync(product.ImageId);
                _db.Images.Remove(image);
            }

            if (product.ProductImages.Any())
            {
                _db.RemoveRange(product.ProductImages);
                _db.Images.RemoveRange(product.ProductImages.Select(x=>x.Image));
            }

            if (product.ProductCategories.Any()) _db.ProductCategories.RemoveRange(product.ProductCategories);
            if (product.CartImages.Any()) _db.CartImages.RemoveRange(product.CartImages);

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

        private async Task<List<BestSellerViewModel>> GetBestSellers()
        {
            var model = new List<BestSellerViewModel>();
            var bestSellers = await _db.Products
                .Include(p => p.Image)
                .Take(3)
                .ToListAsync();
            foreach (var prod in bestSellers)
            {
                var imageSource = GetImgSrc(prod.Image.ImageData, prod.Image.MimeType);
                var price = GetMoney(prod.Styles?.FirstOrDefault()?.Price);
                var viewModel = new BestSellerViewModel(prod.Id, prod.LinkName, prod.LinkName, imageSource, price);
                model.Add(viewModel);
            }
            return model;
        }

        public void Dispose() => _db?.Dispose();
    }
}