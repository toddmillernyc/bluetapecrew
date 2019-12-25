using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> Find(int id);
        Task Update(Category category);
        Task<IEnumerable<Category>> GetAll();
    }
}
