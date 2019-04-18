using System;
using Api.Models;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using Api.Extensions;
using Api.Services.Interfaces;
using Api.ViewModels;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly BtcEntities _context;
        private readonly IImageService _imageService;

        public ProductImagesController(BtcEntities context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        // GET: api/ProductImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductImage>>> GetProductImages()
        {
            return await _context.ProductImages.ToListAsync();
        }

        // GET: api/ProductImages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {  
            try
            {
                var product = await 
                    _context.Products
                        .Include(x=>x.Image)
                        .Include(x=>x.ProductImages)
                        .ThenInclude(x=>x.Image)
                        .FirstOrDefaultAsync(x=>x.Id == id);
                      
                var model = new ProductImagesViewModel(product);
                model.MainImage.Thumbnail = await GetThumbnail(model.MainImage.Data);
                foreach (var item in model.Images)
                {
                    item.Thumbnail = await GetThumbnail(item.Data);
                }
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<byte[]> GetThumbnail(byte[] bytes)
        {
            var resizedImage = await _imageService.ResizeImage(bytes, 75, 100, ImageFormat.Jpeg);
            return resizedImage;
        }

        // PUT: api/ProductImages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductImages(int id, ProductImage productImages)
        {
            if (id != productImages.ImageId)
            {
                return BadRequest();
            }

            _context.Entry(productImages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductImagesExists(id))
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

        // POST: api/ProductImages
        [HttpPost]
        public async Task<ActionResult<ProductImage>> PostProductImages(ProductImage productImages)
        {
            _context.ProductImages.Add(productImages);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductImagesExists(productImages.ImageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductImages", new { id = productImages.ImageId }, productImages);
        }

        // DELETE: api/ProductImages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductImage>> DeleteProductImages(int id)
        {
            var productImages = await _context.ProductImages.FindAsync(id);
            if (productImages == null)
            {
                return NotFound();
            }

            _context.ProductImages.Remove(productImages);
            await _context.SaveChangesAsync();

            return productImages;
        }

        private bool ProductImagesExists(int id)
        {
            return _context.ProductImages.Any(e => e.ImageId == id);
        }
    }
}
