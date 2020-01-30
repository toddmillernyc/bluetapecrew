using System.Threading.Tasks;
using Repositories.Entities;

namespace Repositories.Interfaces
{
    public interface IProductImageRepository
    {
        Task Create(ProductImage productImage);
    }
}
