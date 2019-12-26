using Entities;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Find(int id);
    }
}
