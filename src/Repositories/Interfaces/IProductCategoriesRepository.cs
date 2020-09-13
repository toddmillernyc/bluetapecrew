using System.Threading.Tasks;
using Entities;

namespace Repositories.Interfaces
{
    public interface IProductCategoriesRepository
    {
        Task Create(ProductCategory productCategory);
        Task Delete(ProductCategory productCategory);
    }
}
