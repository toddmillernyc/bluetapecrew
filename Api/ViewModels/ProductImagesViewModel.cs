using Api.Models.Entities;
using System.Collections.Generic;

namespace Api.ViewModels
{
    public class ProductImagesViewModel
    {
        public ProductImagesViewModel(Product product)
        {
            MainImage = new ProductImageViewModel(product.Image);
            foreach (var item in product.ProductImages)
            {
                Images.Add(new ProductImageViewModel(item.Image));
            }
        }

        public ProductImageViewModel MainImage { get; set; }
        public List<ProductImageViewModel> Images { get; set; } = new List<ProductImageViewModel>();
    }

    public class ProductImageViewModel
    {
        public ProductImageViewModel(Image model)
        {
            Id = model.Id;
            Data = model.ImageData;

        }

        public int Id { get; set; }
        public byte[] Data { get; set; }
        public byte[] Thumbnail { get; set; }
    }
}
