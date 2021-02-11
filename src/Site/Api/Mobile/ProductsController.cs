using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;

namespace Site.Api.Mobile
{
    public class MobileProductDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<byte[]> Images { get; set; }
        public IEnumerable<Style> Styles { get; set; }
    }

    [Route("api/mobile/products/{productId}")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<MobileProductDetail> Get(int productId)
        {
            var product = await _productService.FindIncludeAll(productId);
            var model = new MobileProductDetail
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.ProductName,
                Styles = product.Styles
            };
            var images = new List<byte[]> {product.Image.ImageData};
            images.AddRange(product.ProductImages.Select(x => x.Image).Select(image => image.ImageData));
            model.Images = images;
            return model;
        }
    }
}
