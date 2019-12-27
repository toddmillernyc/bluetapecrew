using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BlueTapeCrew.Services
{
    public class ImageService : IImageService
    {
        private readonly IProductService _productService;
        private readonly IImageRepository _imageRepository;

        public ImageService(IProductService productService,
            IImageRepository imageRepository)
        {
            _productService = productService;
            _imageRepository = imageRepository;
        }

        public async Task<Entities.Image> SaveImage(IFormFile file)
        {
            await using var target = new MemoryStream();
            await file.CopyToAsync(target);

            var data = target.ToArray();
            var fileName = file.FileName;
            var c = 0;
            while (true)
            {
                if (!_imageRepository.ImageExists(fileName)) break;
                c++;
                var tokens = file.FileName.Split('.');
                fileName = tokens[0] + "(" + c + ")." + tokens[^1];
            }

            var image = new Entities.Image
            {
                Name = fileName,
                ImageData = data,
                MimeType = file.ContentType
            };

            var ext = file.FileName.Split('.')[1];
            if (ext.Equals("jpg") || ext.Equals("jpeg"))
            {
                image.MimeType = "image/jpeg";
            }
            else if (ext.Equals("png"))
            {
                image.MimeType = "image/png";
            }

            await _imageRepository.Create(image);
            return image;
        }

        public Task Delete(int id) => _imageRepository.Delete(id);

        public async Task<byte[]> ResizeImage(byte[] imageData, int width, int height, ImageFormat format)
        {
            await using var ms = new MemoryStream();
            await using var mss = new MemoryStream();
            await ms.WriteAsync(imageData, 0, imageData.Length);
            var image = Image.FromStream(ms);
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }
            destImage.Save(mss, format);
            return mss.ToArray();
        }

        public async Task<Entities.Image> GetProductImageByName(string name) => await _productService.GetImageBySlug(name.Split('.')[0]);

        public async Task<Entities.Image> GetImageById(int id) => await _imageRepository.Find(id);

    }
}
