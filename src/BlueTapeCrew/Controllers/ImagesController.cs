using BlueTapeCrew.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using Services.Interfaces;

namespace BlueTapeCrew.Controllers
{
    [ResponseCache(Duration = 3600)]
    public class ImagesController : Controller
    {
        private const string JpegMimeType = "image/jpeg";
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [Route("images/{name}")]
        public async Task<ActionResult> Images(string name)
        {
            var image = await _imageService.GetProductImageByName(name);
            return image.ImageData.ToImageResult(image.MimeType);
        }

        public async Task<ActionResult> ProductThumb(int? id)
        {
            if (id == null) return null;
            var imageModel = await _imageService.GetImageById((int) id);
            var resizedImage = await _imageService.ResizeImage(imageModel.ImageData, 75, 100, ImageFormat.Jpeg);
            return resizedImage.ToImageResult(JpegMimeType);
        }
    }
}