using System.Drawing.Imaging;
using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Contracts.Services
{
    public interface IImageService
    {
        Task<byte[]> ResizeImage(byte[] imageData, int width, int height, ImageFormat format);
        Task<Image> GetProductImageByName(string name);
        Task<Image> GetImageById(int id);
    }
}