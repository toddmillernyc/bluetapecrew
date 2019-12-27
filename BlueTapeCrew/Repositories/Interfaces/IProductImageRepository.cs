using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface IProductImageRepository
    {
        Task Create(ProductImage productImage);
    }
}
