using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.Models;

namespace Site.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryPositionsController : ControllerBase
    {
        private readonly BtcEntities _db;

        public CategoryPositionsController(BtcEntities db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UpdateCategoryOrderRequest updatePositionsRequest)
        {
            foreach (var position in updatePositionsRequest.Positions)
            {
                var categoryEntity = await _db.Categories.FindAsync(position.CategoryId);
                categoryEntity.Position = position.Index;
            }
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
