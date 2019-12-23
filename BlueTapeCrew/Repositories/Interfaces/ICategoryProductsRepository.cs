using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface ICategoryProductsRepository
    {
        Task<IEnumerable<Category>> Get();
    }
}
