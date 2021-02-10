using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;

namespace Site.Api.Mobile
{
    [Route("api/mobile/categories/{categoryId}/products")]
    [ApiController]
    public class CategoryProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;

        public CategoryProductsController(
            IProductService productService,
            IImageService imageService)
        {
            _productService = productService;
            _imageService = imageService;
        }

        public async Task<IEnumerable<MobileProduct>> Get(int categoryId)
        {
            var model = new List<MobileProduct>();
            var products = await _productService.GetByCategoryId(categoryId);
            foreach (var product in products)
            {
                var resizedImage = await _imageService.ResizeImage(product.Image.ImageData, 96, 128, ImageFormat.Jpeg);

                model.Add(new MobileProduct
                {
                    Id = product.Id,
                    LowPrice = product.Styles.Min(x=>x.Price),
                    HighPrice = product.Styles.Max(x=>x.Price),
                    Name = product.ProductName,
                    ImageData = resizedImage
                });
            }
            return model;
        }
    }
}
