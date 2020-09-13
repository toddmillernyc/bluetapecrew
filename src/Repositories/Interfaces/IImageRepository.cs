using System.Threading.Tasks;
using Entities;

namespace Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Find(int id);
        Task Create(Image image);
        bool ImageExists(string name);
        Task Delete(int id);
    }
}
