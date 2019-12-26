using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class ImageService : IImageService
    {
        private readonly IProductService _productService;
        private readonly IImageRepository _imageRepository;

        public ImageService(IProductService productService, IImageRepository imageRepository)
        {
            _productService = productService;
            _imageRepository = imageRepository;
        }

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
