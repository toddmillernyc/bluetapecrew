using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> FindBySlugIncludeAll(string name);
        Task<Product> FindBySlugIncludeImage(string name);
        Task<string> GetSlug(int productId);
        Task Delete(int id);
        Task<IEnumerable<Product>> GetProductsWithStylesAndImage(int take);
        Task Create(Product product);
        Task<Product> Find(int id);
        Task<IEnumerable<Product>> GetAllIncludeAll();
        Task Update(Product product);
        Task<Product> FindIncludeAll(int id);
        Task<IEnumerable<Product>> GetByCategoryId(int categoryId);
    }
}