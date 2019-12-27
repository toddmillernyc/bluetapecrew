using Entities;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IImageService
    {
        Task<byte[]> ResizeImage(byte[] imageData, int width, int height, ImageFormat format);
        Task<Image> GetProductImageByName(string name);
        Task<Image> GetImageById(int id);
        Task<Image> SaveImage(IFormFile file);
        Task Delete(int id);
    }
}