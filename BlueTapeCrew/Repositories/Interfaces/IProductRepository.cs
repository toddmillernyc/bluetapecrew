using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> FindBySlugIncludeAll(string name);
        Task<Product> FindBySlugIncludeImage(string name);
        Task<string> GetSlug(int productId);
        Task Delete(int id);
        Task<IList<Product>> GetProductsWithStylesAndImage(int take);
    }
}