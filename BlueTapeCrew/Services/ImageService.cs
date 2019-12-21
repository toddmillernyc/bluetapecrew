using BlueTapeCrew.Models;
using BlueTapeCrew.Services.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlueTapeCrew.Data;
using Image = System.Drawing.Image;

namespace BlueTapeCrew.Services
{
    public class ImageService : IImageService, IDisposable
    {
        private readonly BtcEntities _db;

        public ImageService(BtcEntities db)
        {
            _db = db;
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

        public async Task<Models.Entities.Image> GetProductImageByName(string name)
        {
            var linkName = name.Split('.')[0];
            var product = await _db.Products.Include(x=>x.Image).Where(x => x.LinkName.Equals(linkName)).FirstOrDefaultAsync();
            return product.Image;

        }

        public async Task<Models.Entities.Image> GetImageById(int id)
        {
            return await _db.Images.FindAsync(id);
        }


        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
