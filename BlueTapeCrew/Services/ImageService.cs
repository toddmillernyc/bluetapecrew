using System;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Services;
using BlueTapeCrew.Models;
using Image = System.Drawing.Image;

namespace BlueTapeCrew.Services
{
    public class ImageService : IImageService, IDisposable
    {
        private readonly BtcEntities _db;

        public ImageService()
        {
            _db = new BtcEntities();
        }

        public async Task GenerateCartThumbs()
        {
            foreach (var item in await _db.Products.Where(item => item.Image != null).ToListAsync())
            {
                var cartImg = await _db.CartImages.FirstOrDefaultAsync(x => x.ProductId == item.Id);
                if (cartImg != null)
                {
                    _db.CartImages.Remove(cartImg);
                    await _db.SaveChangesAsync();
                }
                _db.CartImages.Add(new CartImage
                {
                    ProductId = item.Id,
                    ImageData = await ResizeImage(item.Image.ImageData, 37, 34, ImageFormat.Bmp)
                });
                await _db.SaveChangesAsync();
            }
        }

        public async Task<byte[]> ResizeImage(byte[] imageData, int width, int height, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            using (var mss = new MemoryStream())
            {
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

                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel,
                            wrapMode);
                    }
                }
                destImage.Save(mss, format);
                return mss.ToArray();
            }
        }

        public async Task<Models.Image> GetProductImageByName(string name)
        {
            var linkName = name.Split('.')[0];
            var product = await _db.Products.Where(x => x.LinkName.Equals(linkName)).FirstOrDefaultAsync();
            return product.Image;

        }

        public async Task<Models.Image> GetImageById(int id)
        {
            return await _db.Images.FindAsync(id);
        }

        public async Task GenerateAzImages()
        {
            foreach (var item in _db.Products.OrderBy(x => x.ProductName))
            {
                _db.AzImages.Add(new AzImage
                {
                    ImageData = await ResizeImage(item.Image.ImageData, 200, 266, ImageFormat.Jpeg),
                    ProductId = item.Id
                });
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateDimensions()
        {
            foreach (var item in _db.Images)
            {
                var image = Image.FromStream(new MemoryStream(item.ImageData));
                item.Height = (short)image.Height;
                item.Width = (short)image.Width;
                await _db.SaveChangesAsync();
            }
        }

        public async Task ResizeImages()
        {
            foreach (var item in _db.Images)
            {
                if (item.Width == 600 && item.Height == 800) continue;
                var format = ImageFormat.Jpeg;
                if (item.MimeType.Equals("image/png"))
                {
                    format = ImageFormat.Png;
                }
                else if (item.MimeType.Equals("image/gif"))
                {
                    format = ImageFormat.Gif;
                }
                item.ImageData = await ResizeImage(item.ImageData, 600, 800, format);
            }
            await _db.SaveChangesAsync();
            await UpdateDimensions();
        }

        public async Task<CartImage> GetCartImageByProductId(int id)
        {
            return await _db.CartImages.FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<AzImage> GetAzImageByName(string name)
        {
            var linkName = name.Split('.')[0];
            var product = await _db.Products.FirstOrDefaultAsync(x => x.LinkName.Equals(linkName));
            return product.AzImages.FirstOrDefault(x => x.ProductId == product.Id);
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
