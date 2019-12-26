using Entities;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface IProductCategoriesRepository
    {
        Task Create(ProductCategory productCategory);
        Task Delete(ProductCategory productCategory);
    }
}
