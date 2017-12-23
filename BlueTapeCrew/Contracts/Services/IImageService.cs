using System.Drawing.Imaging;
using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Contracts.Services
{
    public interface IImageService
    {
        Task GenerateCartThumbs();
        Task<byte[]> ResizeImage(byte[] imageData, int width, int height, ImageFormat format);
        Task<Image> GetProductImageByName(string name);
        Task<Image> GetImageById(int id);
        Task GenerateAzImages();
        Task UpdateDimensions();
        Task ResizeImages();
        Task<CartImage> GetCartImageByProductId(int id);
        Task<AzImage> GetAzImageByName(string name);
    }
}