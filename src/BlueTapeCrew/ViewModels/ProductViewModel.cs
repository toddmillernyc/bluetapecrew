using System.Collections.Generic;
using System.Linq;
using BlueTapeCrew.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Models;

namespace BlueTapeCrew.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel(Product product, IEnumerable<StyleView> styleViews, IEnumerable<Product> bestSellers, IEnumerable<Category> categories)
        {
            Id = product.Id;
            AdditionalImages = product.ProductImages.Select(x => x.Image).Select(image => image.ToHtmlImageSource());
            AverageReview = product.Reviews.Any() ? product.Reviews.Average(x => x.Rating) : 0;
            BestSellers = bestSellers.Select(p => new BestSellerViewModel(p));
            Category = product.ProductCategories?.FirstOrDefault()?.Category.Name;
            Description = product.Description;
            ImgSource = product.Image.ToHtmlImageSource();
            Menu = categories.Select(cat => new MenuViewModel
            {
                Id = cat.Id,
                Name = cat.Name,
                Items = cat.ProductCategories.Select(x => x.Product).OrderBy(x => x.ProductName).Select(menuItem =>
                    new MenuItemViewModel
                    {
                        Id = menuItem.Id,
                        ItemName = menuItem.ProductName,
                        Slug = menuItem.Slug,
                    })
            });
            Name = product.ProductName;
            Price = $"{product.Styles?.FirstOrDefault()?.Price:n2}";
            Reviews = product.Reviews.OrderByDescending(x => x.DateCreated).Select(review => new ReviewViewModel
            {
                Content = review.ReviewText,
                Date = review.DateCreated.ToString(),
                Id = review.Id,
                Name = review.Name,
                Rating = review.Rating
            });
            StyleId = new SelectList(styleViews, "Id", "StyleText", styleViews.FirstOrDefault());
        }

        public int Id { get; set; }
        public IEnumerable<string> AdditionalImages { get; set; } 
        public decimal AverageReview { get; set; }
        public IEnumerable<BestSellerViewModel> BestSellers { get; set; } 
        public string Category { get; set; }
        public string Description { get; set; }
        public IEnumerable<MenuViewModel> Menu { get; set; }
        public string ImgSource { get; set; }
        public string Name { get; set; }
        public string LinkName { get; set; }
        public string Price { get; set; }
        public IEnumerable<ReviewViewModel> Reviews { get; set; }
        public SelectList StyleId { get; set; }
    }
}