using System.Threading.Tasks;
using Repositories.Entities;

namespace Repositories.Interfaces
{
    public interface IProductCategoriesRepository
    {
        Task Create(ProductCategory productCategory);
        Task Delete(ProductCategory productCategory);
    }
}
