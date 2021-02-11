using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using Site.Services.Interfaces;

namespace Site.Api.Mobile
{
    [Route("api/mobile/products/{productId}")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IViewModelService _productService;

        public ProductsController(IViewModelService productService)
        {
            _productService = productService;
        }

        public async Task<Product> Get(int productId)
        {
            var product = await _productService.GetProductViewModel()
            return product;
        }
    }
}
