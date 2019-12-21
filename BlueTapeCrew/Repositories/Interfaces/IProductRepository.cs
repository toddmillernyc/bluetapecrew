using System.Threading.Tasks;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> FindBySlug(string name);
    }
}