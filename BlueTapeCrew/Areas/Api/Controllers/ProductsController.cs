using System;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace BlueTapeCrew.Areas.Api.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly BtcEntities _db = new BtcEntities();
        private bool ProductExists(int id) => _db.Products.Count(e => e.Id == id) > 0;

        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return _db.Products;
        }

        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(int id)
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

        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != product.Id) return BadRequest();
            _db.Entry(product).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id)) return NotFound();
                throw;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}