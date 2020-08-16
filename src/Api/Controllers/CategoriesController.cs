using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly BtcEntities _db;

        public CategoriesController(BtcEntities db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var categories = await _db.Categories.ToListAsync();
                return Ok(categories.OrderBy(x=>x.Name));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            try
            {
                _db.Categories.Add(category);
                await _db.SaveChangesAsync();
                return Ok(category);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Category category)
        {
            try
            {
                _db.Entry(category).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _db.Categories.FindAsync(id);
                var productCategories = _db.ProductCategories.Where(x => x.CategoryId == id);
                var products = productCategories.Select(x => x.Product);

                if(products.Any())
                    return BadRequest($"Cannot delete category {category.Name}, it contains {products.Count()} product(s).");

                _db.ProductCategories.RemoveRange(productCategories);
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
