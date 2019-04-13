using BlueTapeCrew.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BlueTapeCrew.Areas.Admin.Models;

namespace BlueTapeCrew.Areas.Api.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly BtcEntities _db;

        public CategoriesController(BtcEntities db)
        {
            _db = db;
        }

        public async Task<IEnumerable<AdminCategoryViewModel>> Get()
        {
            var categories = await _db.Categories.OrderBy(category => category.CategoryName).ToListAsync();
            var model = categories.Select(category => new AdminCategoryViewModel
            {
                Id = category.Id,
                Name = category.CategoryName,
                ImageId = category.ImageId,
                Products = category.Products.OrderBy(product => product.ProductName).Select(product =>
                    new AdminProductViewModel
                    {
                        Description = product.Description,
                        Id = product.Id,
                        ImageId = product.ImageId,
                        Name = product.ProductName
                    }).ToList()
            });
            return model;
        }

    }

}
