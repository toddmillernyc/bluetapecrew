using Api.Extensions;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 360)]
    public class ImagesController : ControllerBase
    {
        private const string JpegMimeType = "image/jpeg";
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [Route("images/{name}")]
        public async Task<IActionResult> Images(string name)
        {
            var image = await _imageService.GetProductImageByName(name);
            return image.ImageData.ToImageResult(image.MimeType);
        }

        [Route("images/thumb/{id}")]
        public async Task<IActionResult> ProductThumb(int? id)
        {
            try
            {
                if (id == null) return null;
                var imageModel = await _imageService.GetImageById((int) id);
                var resizedImage = await _imageService.ResizeImage(imageModel.ImageData, 75, 100, ImageFormat.Jpeg);
                return resizedImage.ToImageResult(JpegMimeType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}