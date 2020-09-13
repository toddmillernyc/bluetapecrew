using System.Threading.Tasks;
using Entities;

namespace Repositories.Interfaces
{
    public interface IProductImageRepository
    {
        Task Create(ProductImage productImage);
    }
}
