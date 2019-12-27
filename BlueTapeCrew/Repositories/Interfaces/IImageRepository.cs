using Entities;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Find(int id);
        Task Create(Image image);
        Task<bool> ImageExists(string name);
        Task Delete(int id);
    }
}
