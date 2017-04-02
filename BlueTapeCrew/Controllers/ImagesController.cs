using System.Drawing.Imaging;
using System.Web.Mvc;
using BlueTapeCrew.Models;
using BlueTapeCrew.Utils;
using System.Threading.Tasks;
using BlueTapeCrew.Interfaces;

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

        public async Task<ActionResult> ProductImages(int? id)
        {
            if (id == null) return null;
            var image = await _imageService.GetImageById((int)id);
            return this.Image(image.ImageData, image.MimeType);
        }

        public async Task<ActionResult> ProductThumb(int? id)
        {
            if (id == null) return null;
            var imageModel = await _imageService.GetImageById((int) id);
            return this.Image(await _imageService.ResizeImage(imageModel.ImageData, 75, 100, ImageFormat.Jpeg),"image/jpeg");
        }

        [Route("images/az/{name}")]
        public async Task<ActionResult> AzImage(string name)
        {
            var azImage = await _imageService.GetAzImageByName(name);
            return this.Image(azImage.ImageData, "image/jpeg");
        }

        public ActionResult ImageById(int id)
        {
            using (var db = new BtcEntities())
            {
                var image = db.Images.Find(id);
                return this.Image(image.ImageData, image.MimeType);
            }
        }

        public async Task<ActionResult> CartImage(int productId)
        {
            var image = await _imageService.GetCartImageByProductId(productId);
            return this.Image(image.ImageData, "image/jpg");
        }

        public async Task GenerateAzImages()
        {
            await _imageService.GenerateAzImages();
        }

        public async Task<ActionResult> GenerateCartThumbs()
        {
                await _imageService.GenerateCartThumbs();
                return Content("Cart Images Updated");
        }

        public async Task<ActionResult> UpdateDimensions()
        {
            await _imageService.UpdateDimensions();
            return Content("Image Dimensions Updated");
        }

        public async Task<ActionResult> ResizeImages()
        {
            await _imageService.ResizeImages();
            return Content("Images resized and dimensions updated");
        }

    }
}