using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace BlueTapeCrew.ApiControllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesApiController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesApiController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async  Task<IActionResult> Get()
        {
            var model = await _categoryService.GetAllWithProducts();
            return Ok(model);
        }
    }
}
