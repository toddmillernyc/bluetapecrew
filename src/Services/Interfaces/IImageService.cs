using System.Drawing.Imaging;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces
{
    public interface IImageService
    {
        Task<byte[]> ResizeImage(byte[] imageData, int width, int height, ImageFormat format);
        Task<Image> GetProductImageByName(string name);
        Task<Image> GetImageById(int id);
        Task<Image> SaveImage(SaveImageRequest request);
        Task Delete(int id);
    }
}