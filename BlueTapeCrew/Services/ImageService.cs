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
    public class ImageService : IImageService
    {
        public async Task GenerateCartThumbs()
        {
            using (var db = new BtcEntities())
            {
                foreach (var item in await db.Products.Where(item => item.Image != null).ToListAsync())
                {
                    var cartImg = await db.CartImages.FirstOrDefaultAsync(x => x.ProductId == item.Id);
                    if (cartImg != null)
                    {
                        db.CartImages.Remove(cartImg);
                        await db.SaveChangesAsync();
                    }
                    db.CartImages.Add(new CartImage
                    {
                        ProductId = item.Id,
                        ImageData = await ResizeImage(item.Image.ImageData, 37, 34, ImageFormat.Bmp)
                    });
                    await db.SaveChangesAsync();
                }
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
            using (var db = new BtcEntities())
            {
                var linkName = name.Split('.')[0];
                var product = await db.Products.Where(x => x.LinkName.Equals(linkName)).FirstOrDefaultAsync();
                return product.Image;
            }
        }

        public async Task<Models.Image> GetImageById(int id)
        {
            using (var db = new BtcEntities())
            {
                return await db.Images.FindAsync(id);
            }
        }

        public async Task GenerateAzImages()
        {
            using (var db = new BtcEntities())
            {
                foreach (var item in db.Products.OrderBy(x => x.ProductName))
                {
                    db.AzImages.Add(new AzImage
                    {
                        ImageData = await ResizeImage(item.Image.ImageData, 200, 266, ImageFormat.Jpeg),
                        ProductId = item.Id
                    });
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateDimensions()
        {
            using (var db = new BtcEntities())
            {
                foreach (var item in db.Images)
                {
                    var image = Image.FromStream(new MemoryStream(item.ImageData));
                    item.Height = (short)image.Height;
                    item.Width = (short)image.Width;
                    await db.SaveChangesAsync();
                }

            }
        }

        public async Task ResizeImages()
        {
            using (var db = new BtcEntities())
            {
                foreach (var item in db.Images)
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
                await db.SaveChangesAsync();
                await UpdateDimensions();
            }
        }

        public async Task<CartImage> GetCartImageByProductId(int id)
        {
            using (var db = new BtcEntities())
            {
                return await db.CartImages.FirstOrDefaultAsync(x => x.ProductId == id);
            }
        }

        public async Task<AzImage> GetAzImageByName(string name)
        {
            using (var db = new BtcEntities())
            {
                var linkName = name.Split('.')[0];
                var product = await db.Products.FirstOrDefaultAsync(x => x.LinkName.Equals(linkName));
                return product.AzImages.FirstOrDefault(x => x.ProductId == product.Id);
            }
        }
    }
}