using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;

namespace Site.Api.Mobile
{
    [Route("api/mobile/products/{productId}")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Product> Get(int productId)
        {
            var product = await _productService.FindIncludeAll(productId);
            return product;
        }
    }
}
