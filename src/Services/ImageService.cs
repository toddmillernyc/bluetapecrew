using AutoMapper;
using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Entity = Repositories.Entities;
using Image = System.Drawing.Image;

namespace Services
{
    public class ImageService : IImageService
    {
        private readonly IProductService _productService;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ImageService(IProductService productService,
            IImageRepository imageRepository,
            IMapper mapper)
        {
            _productService = productService;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<Models.Image> SaveImage(SaveImageRequest request)
        {
            await using var target = new MemoryStream(request.ImageData);
            var data = target.ToArray();
            var fileName = request.FileName;
            var c = 0;
            while (true)
            {
                if (!_imageRepository.ImageExists(fileName)) break;
                c++;
                var tokens = request.FileName.Split('.');
                fileName = tokens[0] + "(" + c + ")." + tokens[^1];
            }

            var imageEntity = new Entity.Image
            {
                Name = fileName,
                ImageData = data,
                MimeType = request.ContentType
            };

            var ext = request.FileName.Split('.')[1];
            if (ext.Equals("jpg") || ext.Equals("jpeg"))
            {
                imageEntity.MimeType = "image/jpeg";
            }
            else if (ext.Equals("png"))
            {
                imageEntity.MimeType = "image/png";
            }

            await _imageRepository.Create(imageEntity);
            var model = _mapper.Map<Models.Image>(imageEntity);
            return model;
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

        public async Task<Models.Image> GetProductImageByName(string name)
        {
            var model = await _productService.GetImageBySlug(name.Split('.')[0]);
            return model;
        }

        public async Task<Models.Image> GetImageById(int id)
        {
            var entity = await _imageRepository.Find(id);
            var model = _mapper.Map<Models.Image>(entity);
            return model;
        }

    }
}
