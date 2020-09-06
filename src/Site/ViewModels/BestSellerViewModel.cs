using System.Linq;
using Services.Models;
using Site.Extensions;

namespace Site.ViewModels
{
    public class BestSellerViewModel
    {
        public BestSellerViewModel(Product product)
        {
            Id = product.Id;
            Name = product.ProductName;
            LinkName = product.Slug;
            ImgSource = product.Image.ToHtmlImageSource();
            Price = $"{product.Styles?.FirstOrDefault()?.Price:n2}";
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LinkName { get; set; }
        public string ImgSource { get; set; }
        public string Price { get; set; }
    }
}