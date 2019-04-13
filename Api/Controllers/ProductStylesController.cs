using System;
using Api.Models;
using Api.Models.Entities;
using Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Api.ViewModels;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStylesController : ControllerBase
    {
        private readonly BtcEntities _db;
        private IStyleViewRepository _styleViewRepository;

        public ProductStylesController(BtcEntities context, IStyleViewRepository styleViewRepository)
        {
            _styleViewRepository = styleViewRepository;
            _db = context;
        }

        // DELETE: api/Styles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var style = await _db.Styles.FindAsync(id);
                if (style == null) return NotFound();
                _db.Cart.RemoveRange(await _db.Cart.Where(x => x.StyleId == style.Id).ToListAsync());
                _db.Styles.Remove(style);
                await _db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStyles(int id)
        {
            try
            {
                var model = new ProductStylesViewModel
                {
                    Styles = await _styleViewRepository.GetBy(id),
                    Colors = await _db.Colors.OrderBy(x => x.Id).ToListAsync(),
                    Sizes = await _db.Sizes.OrderBy(x => x.SizeOrder).ToListAsync()
                };
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Styles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStyles(int id, Style styles)
        {
            if (id != styles.Id)
            {
                return BadRequest();
            }

            _db.Entry(styles).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StylesExists(id))
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

        // POST: api/Styles
        [HttpPost]
        public async Task<ActionResult<Style>> PostStyles(Style styles)
        {
            _db.Styles.Add(styles);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetStyles", new { id = styles.Id }, styles);
        }



        private bool StylesExists(int id)
        {
            return _db.Styles.Any(e => e.Id == id);
        }
    }
}
