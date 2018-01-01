using System.Drawing.Imaging;
using System.Web.Mvc;
using BlueTapeCrew.Utils;
using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Services;

namespace BlueTapeCrew.Controllers
{
    [OutputCache(Duration = 3600)]
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        // GET: Images
        [Route("images/{name}")]
        public async Task<ActionResult> Images(string name)
        {
            var image = await _imageService.GetProductImageByName(name);
            return this.Image(image.ImageData, image.MimeType);
        }

        public async Task<ActionResult> ProductThumb(int? id)
        {
            if (id == null) return null;
            var imageModel = await _imageService.GetImageById((int) id);
            return this.Image(await _imageService.ResizeImage(imageModel.ImageData, 75, 100, ImageFormat.Jpeg),"image/jpeg");
        }

    }
}