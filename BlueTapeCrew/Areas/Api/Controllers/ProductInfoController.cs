using System;
using System.Threading.Tasks;
using System.Web.Http;
using BlueTapeCrew.Areas.Api.Models;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Areas.Api.Controllers
{
    public class ProductInfoController : ApiController
    {
        private readonly BtcEntities _db = new BtcEntities();

        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var product = await _db.Products.FindAsync(id);
                return Ok(product);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IHttpActionResult> Put(int id, ProductInfo model)
        {
            try
            {
                var entity = await _db.Products.FindAsync(id);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
