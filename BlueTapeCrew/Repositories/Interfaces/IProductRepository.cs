using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> FindBySlug(string name);
        Task<string> GetSlug(int productId);
        Task Delete(int id);
        Task<IList<Product>> GetProductsWithStylesAndImage(int take);
    }
}