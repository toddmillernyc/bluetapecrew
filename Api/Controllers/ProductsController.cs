using Api.Models;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly BtcEntities _db;

        public ProductsController(BtcEntities db)
        {
            _db = db;
        }

        private bool ProductExists(int id) => _db.Products.Count(e => e.Id == id) > 0;

        public async Task<IActionResult> Get()
        {
            return Ok(await _db.Products.ToListAsync());
        }

        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var product = await _db.Products.FindAsync(id);
                if (product == null) return NotFound();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _db.Entry(product).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        public async Task<IActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return Ok(product);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
        }
    }
}