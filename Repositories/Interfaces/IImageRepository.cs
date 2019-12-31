using System.Threading.Tasks;
using Repositories.Entities;

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
