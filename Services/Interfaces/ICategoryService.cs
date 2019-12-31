using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> Find(int id);
        Task ChangeName(int categoryId, string name);
        Task Delete(int id);
        Task Create(Category category);
        Task TogglePublish(int id);
        Task AddProductCategory(ProductCategory productCategory);
        Task<IEnumerable<Category>> GetAllWithProducts();
        Task DeleteProductCategory(ProductCategory productCategory);
        Task<IEnumerable<Category>> GetAllPublishedWithProductsAndStyles();
        Task<IEnumerable<Category>> GetAllPublishedWithProducts();
    }
}
