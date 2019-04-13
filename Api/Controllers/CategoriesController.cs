using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly BtcEntities _db;
        public CategoriesController(BtcEntities db)
        {
            _db = db;
        }
        public async Task<IActionResult>Get()
        {
            var categories = 
                await _db.Categories
                    .Include(x=>x.ProductCategories)
                    .ThenInclude(x=>x.Product)
                    .OrderBy(category => category.CategoryName)
                    .ToListAsync();
            var model = categories.Select(category => new AdminCategoryViewModel
            {
                Id = category.Id,
                Name = category.CategoryName,
                ImageId = category.ImageId,
                Products = category.ProductCategories.Select(x=>x.Product).OrderBy(product => product.ProductName).Select(product =>
                    new AdminProductViewModel
                    {
                        Description = product.Description,
                        Id = product.Id,
                        ImageId = product.ImageId,
                        Name = product.ProductName
                    }).ToList()
            });
            return Ok(model);
        }
    }
}