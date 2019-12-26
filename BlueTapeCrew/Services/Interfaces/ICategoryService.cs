using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> Find(int id);
        Task ChangeName(int categoryId, string name);
        Task<IEnumerable<Category>> GetAll();
        Task Delete(int id);
        Task Create(Category category);
        Task TogglePublish(int id);
    }
}
